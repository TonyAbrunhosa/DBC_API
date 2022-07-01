using Microsoft.Extensions.DependencyInjection;

namespace DigitalBrasilCash.API.Configurations
{
    public static class Cors
    {
        public readonly static string origins = "AllowSpecificOrigins";
        public static void ResolveCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(origins, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
