using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Shared.Utilities;
using FluentValidation;

namespace DigitalBrasilCash.Domain.Accounts.Validation
{
    public class AccountInputQueryValidate : AbstractValidator<AccountInput>
    {
        public AccountInputQueryValidate()
        {
            RuleFor(x => x.name).NotEmpty().NotNull().WithMessage("Informe o Nome.").MinimumLength(3).MaximumLength(60);
            RuleFor(x => x.tax_id).NotEmpty().NotNull().WithMessage("Informe o CPF/CNPJ.");            

            RuleFor(x => x).Custom((o, context) =>
            {
                if (!string.IsNullOrEmpty(o.tax_id))
                    if (!o.tax_id.IsOkCpf())
                        context.AddFailure("tax_id", "Informe um CPF/CNPJ válido.");                             
            });
        }
    }
}
