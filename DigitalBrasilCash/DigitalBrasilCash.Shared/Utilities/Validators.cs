using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Shared.Utilities
{
    public static class Validators
    {
        public static bool IsOkCpfCnpj(this string valor)
        {
            if (valor.Length == 11)
                return valor.IsOkCpf();
            else if (valor.Length == 14)
                return valor.IsOkCnpj();
            else
                return false;
        }
        public static bool IsOkCpf(this string valor)
        {
            if (valor.Length == 11)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                valor = valor.Trim();
                valor = valor.Replace(".", "").Replace("-", "");
                if ((valor.Length != 11) || (valor == 00000000000.ToString()) || (valor == 11111111111.ToString()) || (valor == 22222222222.ToString()) || (valor == 33333333333.ToString()) || (valor == 44444444444.ToString()) || (valor == 55555555555.ToString()) || (valor == 66666666666.ToString()) || (valor == 77777777777.ToString()) || (valor == 88888888888.ToString()) || (valor == 99999999999.ToString()))
                {
                    return false;
                }

                tempCpf = valor.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();

                return valor.EndsWith(digito);
            }
            else
                return false;
        }

        public static bool IsOkCnpj(this string valor)
        {
            if (valor.Length == 14)
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                valor = valor.Trim();
                valor = valor.Replace(".", "").Replace("-", "").Replace("/", "");
                if (valor.Length != 14)
                    return false;
                tempCnpj = valor.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();

                return valor.EndsWith(digito);
            }
            else
                return false;
        }
    }
}
