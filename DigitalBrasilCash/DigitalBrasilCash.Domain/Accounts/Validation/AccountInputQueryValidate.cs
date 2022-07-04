using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Shared.Utilities;
using FluentValidation;

namespace DigitalBrasilCash.Domain.Accounts.Validation
{
    public class AccountInputQueryValidate : AbstractValidator<AccountInput>
    {
        public AccountInputQueryValidate()
        {
            RuleFor(x => x.name).MinimumLength(3).MaximumLength(60);            

            RuleFor(x => x).Custom((o, context) =>
            {
                if (!string.IsNullOrEmpty(o.tax_id))
                    if (!o.tax_id.IsOkCpfCnpj())
                        context.AddFailure("tax_id", "Informe um CPF/CNPJ válido.");                             
            });
        }
    }
}
