using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Token.Input;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Contracts.Services
{
    public interface ITokenService
    {
        Task<ICommandResult> ObterToken(TokenInput input);
    }
}
