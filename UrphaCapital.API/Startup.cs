using Microsoft.AspNetCore.Identity;
using Serilog;
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

            var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configRoot)
               .Enrich.FromLogContext()
               .CreateLogger();
            logging.AddSerilog(logger);

            services.AddControllers();

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

            app.UseCors();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
