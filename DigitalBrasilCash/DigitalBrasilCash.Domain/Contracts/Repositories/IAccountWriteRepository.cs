using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Entity;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Contracts.Repositories
{
    public interface IAccountWriteRepository
    {
        Task<int> Novo(AccountEntity account);
        Task NovoLocal(AccountEntity account);
    }
}
