using DigitalBrasilCash.Domain.Contracts.Command;
using System;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Contracts.Services
{
    public interface IAccountQueryService
    {
        Task<ICommandResult> Buscar(string name, string tax_id, DateTime? created_at);        
    }
}
