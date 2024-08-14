
using UrphaCapital.Application;
using UrphaCapital.Infrastructure;

namespace UrphaCapital.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration, builder.Logging);
            startup.ConfigureServices(builder.Services, builder.Logging);

            var app = builder.Build();

            startup.Configure(app, builder.Environment);
        }
    }
}
