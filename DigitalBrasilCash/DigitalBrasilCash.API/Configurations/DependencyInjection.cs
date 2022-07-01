using DigitalBrasilCash.Shared.Communication;
using DigitalBrasilCash.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBrasilCash.API.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<AppSettings, AppSettings>();
            services.AddScoped<SqlCommunication, SqlCommunication>();

            ////SERVICES
            //services.AddTransient<ITokenService, TokenService>();

            ////REPOSITORY
            //services.AddTransient<ITokenRepository, TokenRepository>();

        }
    }
}

