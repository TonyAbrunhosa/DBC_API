using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Accounts.Output;
using DigitalBrasilCash.Domain.Accounts.Validation;
using DigitalBrasilCash.Domain.Command;
using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Contracts.Services;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Domain.ViaCep;
using DigitalBrasilCash.Shared.Communication;
using DigitalBrasilCash.Shared.Utilities;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Application.Services
{
    public class AccountWriteService : IAccountWriteService
    {
        private readonly IAccountWriteRepository _accountWriteRepository;
        private readonly AccountInputValidate _inputValidate;

        public AccountWriteService(IAccountWriteRepository accountWriteRepository, AccountInputValidate inputValidate)
        {
            _accountWriteRepository = accountWriteRepository;
            _inputValidate = inputValidate;
        }

        public async Task<ICommandResult> Novo(AccountInput input)
        {
            var retorno = _inputValidate.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            var entity = new AccountEntity(input.name, input.tax_id, input.password, input.phone_number, input.postal_code);
            var id_account = await _accountWriteRepository.Novo(entity);
            var output = new AccountOutput(id_account, entity.Name, entity.Tax_id, entity.Phone_number, entity.Status, entity.Postal_code, entity.Created_at);

            if (!string.IsNullOrEmpty(output.Postal_code))
            {
                var viaCep = await ObterAddressViaCep(output.Postal_code);
                output.AddAdress(viaCep.Logradouro, viaCep.Bairro, viaCep.Logradouro, viaCep.Uf);
            }

            return new CommandResult(true, "Cadastro realizado com sucesso!", output);
        }  

        private async Task<ViaCepInput> ObterAddressViaCep(string postal_code)
        {
            return await CommunicationExternal.Get<ViaCepInput>(@$"https://viacep.com.br/ws/{postal_code}/json/");            
        }        
    }
}
