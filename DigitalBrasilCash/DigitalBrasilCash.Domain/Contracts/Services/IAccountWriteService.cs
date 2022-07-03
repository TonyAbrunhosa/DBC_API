using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Contracts.Command;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Contracts.Services
{
    public interface IAccountWriteService
    {
        Task<ICommandResult> Novo(AccountInput input);
    }
}
