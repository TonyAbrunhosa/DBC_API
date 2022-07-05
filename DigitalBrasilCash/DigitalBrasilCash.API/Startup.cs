using DigitalBrasilCash.API.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;


namespace DigitalBrasilCash.API
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveDependencyInjection();
            services.ResolveCors();
            services.AddAuthenticationBearer();
            services.AddSwagger("DigitalBrasilCash.Api", "v1");
            services.AddCompression();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).AddFluentValidation();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalBrasilCash.Api v1"); });
            app.UseRouting();
            app.UseCors(Cors.origins);
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
