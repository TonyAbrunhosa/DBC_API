using DigitalBrasilCash.Domain.Command;
using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Contracts.Services;
using DigitalBrasilCash.Domain.ViaCep;
using DigitalBrasilCash.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Application.Services
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IAccountQueryRepository _queryRepository;

        public AccountQueryService(IAccountQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<ICommandResult> Buscar(string name, string tax_id, DateTime? created_at)
        {
            var lstAccounts = await _queryRepository.Buscar(name, tax_id, created_at);

            foreach (var acc in lstAccounts)
            {
                if (!string.IsNullOrEmpty(acc.Postal_code))
                {
                    var viaCep = await ObterAddressViaCep(acc.Postal_code);
                    acc.AddAdress(viaCep.Logradouro, viaCep.Bairro, viaCep.Logradouro, viaCep.Uf);
                }
            }

            return new CommandResult(true, "Consulta realicada com sucesso!", lstAccounts);
        }

        private async Task<ViaCepInput> ObterAddressViaCep(string postal_code)
        {
            return await CommunicationExternal.Get<ViaCepInput>(@$"https://viacep.com.br/ws/{postal_code}/json/");
        }
    }
}
