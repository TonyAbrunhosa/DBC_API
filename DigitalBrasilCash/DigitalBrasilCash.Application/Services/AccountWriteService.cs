using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Accounts.Output;
using DigitalBrasilCash.Domain.Accounts.Validation;
using DigitalBrasilCash.Domain.Command;
using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Contracts.Repositories;
using DigitalBrasilCash.Domain.Contracts.Services;
using DigitalBrasilCash.Domain.Entity;
using DigitalBrasilCash.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Application.Services
{
    public class AccountWriteService : IAccountWriteService
    {
        private readonly IAccountWriteRepository _accountWriteRepository;
        private readonly AccountInputValidate _inputValidate;
        private List<AccountEntity> _ListAccountEntities;

        public AccountWriteService(IAccountWriteRepository accountWriteRepository, List<AccountEntity> listAccountEntities, AccountInputValidate inputValidate)
        {
            _accountWriteRepository = accountWriteRepository;
            _ListAccountEntities = listAccountEntities;
            _inputValidate = inputValidate;
        }

        public async Task<ICommandResult> Novo(AccountInput input)
        {
            var retorno = _inputValidate.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            var entity = CriarObejtoAccountEntity(input);
            entity.id_account = await _accountWriteRepository.Novo(entity);
            var output = await CriarObjetoAccountOutput(entity);

            return new CommandResult(true, "Cadastro realizado com sucesso!", output);
        }
        
        public async Task<ICommandResult> NovoLocal(AccountInput input)
        {
            var retorno = _inputValidate.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            var entity = CriarObejtoAccountEntity(input);
            entity.id_account = (_ListAccountEntities?.Count ?? 0) + 1;
            await _accountWriteRepository.NovoLocal(entity);
            var output = await CriarObjetoAccountOutput(entity);

            return new CommandResult(true, "Cadastro realizado com sucesso!", output);
        }

        private async Task<AccountOutput> CriarObjetoAccountOutput(AccountEntity entity)
        {
            return new AccountOutput
            {
                name = entity.name,
                tax_id = entity.tax_id,
                phone_number = entity.phone_number,                
                status = string.IsNullOrEmpty(entity.postal_code) ? "pending" : "approved",
                postal_code = entity.postal_code,
                created_at = entity.created_at,
                address = await ObterAddress(entity.postal_code)
            };
        }

        private async Task<AddressOutput> ObterAddress(string postal_code)
        {
            throw new NotImplementedException();
        }

        private AccountEntity CriarObejtoAccountEntity(AccountInput input)
        {
            return new AccountEntity
            {
                name = input.name,
                tax_id = input.tax_id,
                phone_number = input.phone_number,
                password = input.password,
                status = string.IsNullOrEmpty(input.postal_code) ? "pending" : "approved",
                postal_code = input.postal_code,
                created_at = DateTime.Now
            };
        }
    }
}
