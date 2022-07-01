using System;
using Microsoft.Extensions.Configuration;

namespace DigitalBrasilCash.Shared.Utilities
{
    public class AppSettings
    {
        private readonly IConfiguration _config;

        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        public string Get(string key)
        {
            var value = Environment.GetEnvironmentVariable(key.ToUpper());

            if (string.IsNullOrEmpty(value))
                value = _config.GetSection(key).Value;

            return value;
        }


        public string ConnectionStrings(string key)
        {
            var value = AdmHash.Decrypt(Environment.GetEnvironmentVariable(key));

            if (string.IsNullOrEmpty(value))
                return AdmHash.Decrypt(_config.GetSection(key)?.Value?.ToString() ?? "");

            return value;
        }
    }
}
