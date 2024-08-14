using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrphaCapital.Application.AuthServices;
using UrphaCapital.Application.HasherServices;

namespace UrphaCapital.Application
{
    public static class UrphaCapitalApplicationDependencyInjection
    {
        public static IServiceCollection AddUrphaCapitalApplicationDependencyInjection(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
