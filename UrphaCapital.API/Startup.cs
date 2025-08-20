﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configRoot["JWTSettings:ValidIssuer"],
                        ValidAudience = configRoot["JWTSettings:ValidAudence"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configRoot["JWTSettings:Secret"]))
                    };
                });

            services.AddAuthorization();

            services.AddOptions();
            
            services.AddHttpClient();
            
            
            services.AddUrphaCapitalApplicationDependencyInjection();
            services.AddUrphaCapitalInfrastructureDependencyInjection(configRoot);


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


            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1_000_000_000;
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true; 
            });

            services.AddMemoryCache();

            services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Urpha Capital",
                    Description = "Web api for controlling Urpha capital web",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

            });


        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(u =>
                {
                    u.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

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
