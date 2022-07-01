using Dapper;
using Dapper.Contrib.Extensions;
using DigitalBrasilCash.Shared.Utilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Shared.Communication
{
    public class SqlCommunication
    {
        private readonly ILogger<SqlCommunication> _logger;
        private readonly AppSettings _configuration;

        public SqlCommunication(AppSettings configuration, ILogger<SqlCommunication> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<T> QueryFirstOrDefaultAsyncDapper<T>(string sqlStr, object param = null, int timeout = 900) where T : new()
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    return await Conn.QueryFirstOrDefaultAsync<T>(sqlStr, param, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
                return default(T);
            }

        }
        public async Task<IEnumerable<T>> QueryAsyncDapper<T>(string sqlStr, object param = null, int timeout = 900) where T : new()
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    return await Conn.QueryAsync<T>(sqlStr, param, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
                return null;
            }
        }

        public async Task<int> InsertAsyncDapper<T>(T obj, int timeout = 900) where T : class
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (string.IsNullOrEmpty(ConnectionString))
                _logger.LogInformation("Não foi encontrada a ConnectionString.");

            using (var Conn = new SqlConnection(ConnectionString))
                return await Conn.InsertAsync(obj, commandTimeout: timeout);
        }
        public async Task InsertAsyncDapper<T>(List<T> obj, int timeout = 900) where T : class
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    await Conn.InsertAsync(obj, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
            }
        }

        public async Task UpdateAsyncDapper<T>(T obj, int timeout = 900) where T : class
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    await Conn.UpdateAsync(obj, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
            }
        }
        public async Task UpdateAsyncDapper<T>(List<T> obj, int timeout = 900) where T : class
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    await Conn.UpdateAsync(obj, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
            }
        }

        public async Task RemoveAsyncDapper<T>(T obj, int timeout = 900) where T : class
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (!string.IsNullOrEmpty(ConnectionString))
                using (var Conn = new SqlConnection(ConnectionString))
                    await Conn.DeleteAsync(obj, commandTimeout: timeout);
            else
            {
                _logger.LogInformation("Não foi encontrada a ConnectionString.");
            }
        }

        public async Task ExcuteAsyncDapper(string strSql, object param = null, int timeout = 900)
        {
            var ConnectionString = _configuration.ConnectionStrings("RSautoDb");

            if (string.IsNullOrEmpty(ConnectionString))
                _logger.LogInformation("Não foi encontrada a ConnectionString.");

            using (var Conn = new SqlConnection(ConnectionString))
                await Conn.ExecuteAsync(strSql, param, commandTimeout: timeout);
        }
        public string GetConnectionString()
        {
            return _configuration.ConnectionStrings("RSautoDb");
        }
    }
}
