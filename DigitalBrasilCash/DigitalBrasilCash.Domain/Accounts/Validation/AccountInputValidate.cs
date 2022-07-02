using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Shared.Utilities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DigitalBrasilCash.Domain.Accounts.Validation
{
    public class AccountInputValidate: AbstractValidator<AccountInput>
    {
        public AccountInputValidate()
        {
            RuleFor(x => x.name).NotEmpty().NotNull().WithMessage("Informe o Nome.").MinimumLength(3).MaximumLength(60);
            RuleFor(x => x.tax_id).NotEmpty().NotNull().WithMessage("Informe o CPF/CNPJ.");
            RuleFor(x => x.password).NotEmpty().NotNull().WithMessage("Informe a Senha.").MinimumLength(8).MaximumLength(12);

            RuleFor(x => x).Custom((o, context) =>
            {
                if (!string.IsNullOrEmpty(o.tax_id))
                    if (o.tax_id.IsOkCpf())
                    {
                        context.AddFailure("tax_id", "Informe um documento válido.");
                    }
                if (!string.IsNullOrEmpty(o.postal_code))
                {
                    Regex Rgx = new Regex(@"^\d{5}-\d{3}$");

                    if (!Rgx.IsMatch(o.postal_code))
                        context.AddFailure("postal_code", "Informe um Cep válido.");
                
                }   
            });
        }
    }
}
