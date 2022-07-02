using DigitalBrasilCash.Domain.Accounts.Output;
using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Shared.Communication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Infrastructure.Repositories
{
    public class AccountQueryRepository : IAccountQueryRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<AccountWriteRepository> _logger;
        private List<AccountEntity> _ListAccountEntities;

        public AccountQueryRepository(SqlCommunication sql, ILogger<AccountWriteRepository> logger, List<AccountEntity> listAccountEntities)
        {
            _sql = sql;
            _logger = logger;
            _ListAccountEntities = listAccountEntities;
        }

        public async Task<IEnumerable<AccountOutput>> Buscar(string name, string tax_id, DateTime created_at)
        {
            return await _sql.QueryAsyncDapper<AccountOutput>(@"
                BEGIN
                    SELECT 
                        name,
                        tax_id,
                        phone_number,
                        created_at,
                        postal_code
                    FROM ACCOUNT WITH(NOLCOK)
                    WHERE (@name = '' OR name = @name)
                    AND (@tax_id = '' OR tax_id = @tax_id)
                    AND (@created_at = '' OR created_at = @created_at)
                END
            ", new { name = name, tax_id = tax_id, created_at = created_at });
        }

        public Task<IEnumerable<AccountOutput>> Buscarlocal(string name, string tax_id, DateTime created_at)
        {
            throw new NotImplementedException();
        }
    }
}
