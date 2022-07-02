using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Shared.Communication;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Infrastructure.Repositories
{
    public class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<AccountWriteRepository> _logger;
        private List<AccountEntity> _ListAccountEntities;

        public AccountWriteRepository(SqlCommunication sql, ILogger<AccountWriteRepository> logger, List<AccountEntity> listAccountEntities)
        {
            _sql = sql;
            _logger = logger;
            _ListAccountEntities = listAccountEntities;
        }

        public async Task<int> Novo(AccountEntity account)
        {
            _logger.LogInformation("AccountWriteRepository - Metodo: Novo, Inserindo novo registro.");
            return await _sql.InsertAsyncDapper(account);
        }

        public async Task NovoLocal(AccountEntity account)
        {
            _logger.LogInformation("AccountWriteRepository - Metodo: Novo, Inserindo novo registro.");
            await Task.Run(() =>
            {
                _ListAccountEntities.Add(account);                
            });
            
        }
    }
}
