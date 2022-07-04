using DigitalBrasilCash.Application.Services;
using DigitalBrasilCash.Domain.Accounts.Validation;
using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Contracts.Services;
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
            services.AddTransient<IAccountWriteService, AccountWriteService>();
            services.AddTransient<IAccountQueryService, AccountQueryService>();

            //REPOSITORY
            //services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IAccountWriteRepository, AccountWriteRepository>();
            services.AddTransient<IAccountQueryRepository, AccountQueryRepository>();

            // VALIDATORS
            services.AddTransient<AccountInputValidate, AccountInputValidate>();
            services.AddTransient<AccountInputQueryValidate, AccountInputQueryValidate>();            
        }
    }
}

