using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.ErrorSender;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.ExternalServices.PaymentProcessing;
using UrphaCapital.Application.UseCases.StudentsCRUD.Handlers.QueryHandler;

namespace UrphaCapital.Application
{
    public static class UrphaCapitalApplicationDependencyInjection
    {
        public static IServiceCollection AddUrphaCapitalApplicationDependencyInjection(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IErrorSenderService, ErrorSenderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
