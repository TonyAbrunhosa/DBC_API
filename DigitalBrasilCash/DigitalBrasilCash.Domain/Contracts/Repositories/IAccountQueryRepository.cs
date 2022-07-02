using DigitalBrasilCash.Domain.Accounts.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Contracts.Repositories
{
    public interface IAccountQueryRepository
    {
        Task<IEnumerable<AccountOutput>> Buscar(string name, string tax_id, DateTime created_at);
        Task<IEnumerable<AccountOutput>> Buscarlocal(string name, string tax_id, DateTime created_at);
    }
}
