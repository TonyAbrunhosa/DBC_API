using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalBrasilCash.API.Configurations
{
    public static class Authentication
    {
        public static IServiceCollection AddAuthenticationBearer(this IServiceCollection services)
        {
            string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "supersecretkey.json");
            if (!File.Exists(MyJwkLocation))
            {
                var newKey = CreateJWK();
                File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            }
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
                   ValidIssuer = "DigitalBrasilCash",
                   ValidAudience = "DigitalBrasilCash",
                   IssuerSigningKey = JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation) ?? default)
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
        private static JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }
        private static byte[] GenerateKey(int bytes)
        {
            RandomNumberGenerator Rng = RandomNumberGenerator.Create();
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }

    }
}
