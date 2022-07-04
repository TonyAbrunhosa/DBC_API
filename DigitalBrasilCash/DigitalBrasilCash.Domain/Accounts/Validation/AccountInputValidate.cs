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
                    if (!o.tax_id.IsOkCpf())
                        context.AddFailure("tax_id", "Informe um CPF/CNPJ válido.");

                if (!string.IsNullOrEmpty(o.postal_code))
                {
                    Regex Rgx = new Regex(@"^\d{8}$");                    

                    if (!Rgx.IsMatch(o.postal_code.Replace("-", "")))
                        context.AddFailure("postal_code", "Informe um Cep válido. Exemplo: 99999-999.");
                
                }

                if (!string.IsNullOrEmpty(o.phone_number))
                {
                    bool error = false;
                    string postaCode = o.postal_code.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    if (o.phone_number.Length == 11)
                    {
                        Regex Rgx = new Regex(@"^\d{11}$");
                        if (!Rgx.IsMatch(postaCode))
                            error = true;
                    }
                    else if (o.phone_number.Length == 10)
                    {
                        Regex Rgx = new Regex(@"^\d{10}$");
                        if (!Rgx.IsMatch(postaCode))
                            error = true;
                    }
                    else
                        error = true;

                    if(error)
                        context.AddFailure("postal_code", "Informe um Telefone válido. Exemplo: 11999999999 ou 1199999999.");

                }
            });
        }
    }
}
