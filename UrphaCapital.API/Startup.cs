using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Text.Json.Serialization;
using Telegram.Bot;
using UrphaCapital.API.Middlewares;
using UrphaCapital.Application;
using UrphaCapital.Infrastructure;

namespace UrphaCapital.API
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }

        public ILoggingBuilder logging
        {
            get; set;
        }

        public Startup(IConfiguration configuration, ILoggingBuilder logging)
        {
            configRoot = configuration;
            this.logging = logging;
        }
        public void ConfigureServices(IServiceCollection services, ILoggingBuilder Logging)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddUrphaCapitalApplicationDependencyInjection();
            services.AddUrphaCapitalInfrastructureDependencyInjection(configRoot);


            services.AddSingleton<TelegramBotClient>(provider =>
            {
                var botToken = $"{configRoot.GetSection("TelegramBot").GetSection("API").Value}";
                return new TelegramBotClient(botToken);
            });


            var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configRoot)
               .Enrich.FromLogContext()
               .CreateLogger();
            logging.AddSerilog(logger);

            services.AddRateLimiter(x =>
            {
                x.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                x.AddSlidingWindowLimiter("sliding", options =>
                {
                    options.Window = TimeSpan.FromSeconds(60);
                    options.SegmentsPerWindow = 6;
                    options.PermitLimit = 60;
                    options.QueueLimit = 10;
                });
            });

            services.AddMemoryCache(options => options.SizeLimit = 2048);

            services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseRateLimiter();

            app.UseCors();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
