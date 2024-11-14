using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.EmailSenderServices;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.ExternalServices.OTPServices;
using UrphaCapital.Application.ExternalServices.PaymentProcessing;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Application.UseCases.GlobalIdServices;

namespace UrphaCapital.Application
{
    public static class UrphaCapitalApplicationDependencyInjection
    {
        public static IServiceCollection AddUrphaCapitalApplicationDependencyInjection(this IServiceCollection services)
        {

            services.AddMediatR(typeof(GetAllCoursesQuery).Assembly);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IGlobalIdService, GlobalIdService>();

            return services;
        }
    }
}
