using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Shared.Communication;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Infrastructure.Repositories
{
    public class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<AccountWriteRepository> _logger;

        public AccountWriteRepository(SqlCommunication sql, ILogger<AccountWriteRepository> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        public async Task<int> Novo(AccountEntity account)
        {
            _logger.LogInformation("AccountWriteRepository - Metodo: Novo, Inserindo novo registro.");
            return await _sql.InsertAsyncDapper(account);
        }        
    }
}
