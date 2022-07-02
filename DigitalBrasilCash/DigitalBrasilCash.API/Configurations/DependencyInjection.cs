using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Infrastructure.Repositories;
using DigitalBrasilCash.Shared.Communication;
using DigitalBrasilCash.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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

            //REPOSITORY
            //services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddSingleton<IAccountWriteRepository, AccountWriteRepository>();
            services.AddSingleton<IAccountQueryRepository, AccountQueryRepository>();

            services.AddTransient<List<AccountEntity>, List<AccountEntity>>();
        }
    }
}

