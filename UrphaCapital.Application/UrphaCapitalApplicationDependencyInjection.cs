using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrphaCapital.Application.AuthServices;
using UrphaCapital.Application.ErrorSender;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.PaymentProcessing;
using UrphaCapital.Application.UseCases.StudentsCRUD.Handlers.QueryHandler;

namespace UrphaCapital.Application
{
    public static class UrphaCapitalApplicationDependencyInjection
    {
        public static IServiceCollection AddUrphaCapitalApplicationDependencyInjection(this IServiceCollection services)
        {

            services.AddMediatR(typeof(GetStudentByEmailQueryHandler).Assembly);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IErrorSenderService, ErrorSenderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
