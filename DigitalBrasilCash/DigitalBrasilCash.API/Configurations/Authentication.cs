using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalBrasilCash.API.Configurations
{
    public static class Authentication
    {
        public static IServiceCollection AddAuthenticationBearer(this IServiceCollection services)
        {
            string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "mysupersecretkey.json");
            var Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "RSauto",
                   ValidAudience = "RSauto",
                   IssuerSigningKey = JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation))
               };

               options.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       return Task.CompletedTask;
                   },
                   OnTokenValidated = context =>
                   {
                       return Task.CompletedTask;
                   }
               };
           });

            return services;
        }
    }
}
