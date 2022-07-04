using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Accounts.Validation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace TestBrasilCash.API
{
    [TestFixture]
    public class TesteAccountInput
    {
        private AccountInputValidate validator;

        [SetUp]
        public void Setup()
        {
            validator = new AccountInputValidate();
        }

        [Test]
        public void Should_have_error_when_Name_is_null()
        {
            var model = new AccountInput { name = null, tax_id = "12345678909", password="teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.name);
        }
        [Test]
        public void Should_have_error_when_Name_Minimum()
        {
            var model = new AccountInput { name = "", tax_id = "12345678909", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.name);
        }
        [Test]
        public void Should_have_error_when_Name_MaximumLength()
        {
            var model = new AccountInput { name = "Teste name account MaximumLength - Teste name account MaximumLength", tax_id = "12345678909", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.name);
        }

        [Test]
        public void Should_have_error_when_TaxId_is_null()
        {
            var model = new AccountInput { name = "Teste", tax_id = null, password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.tax_id);
        }
        [Test]
        public void Should_have_error_when_TaxId_CPF_Validate()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678901", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.tax_id);
        }
        [Test]
        public void Is_valid_when_TaxId_CPF()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.tax_id);
        }
        [Test]
        public void Should_have_error_when_TaxId_CNPJ_Validate()
        {
            var model = new AccountInput { name = "Teste", tax_id = "2498776300136", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.tax_id);
        }
        [Test]
        public void Is_valid_when_TaxId_CNPJ()
        {
            var model = new AccountInput { name = "Teste", tax_id = "98259543000122", password = "teste123" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.tax_id);
        }

        [Test]
        public void Should_have_error_when_Password_is_null()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.password);
        }

        [Test]
        public void Should_have_error_when_Password_MinimumLength()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "teste" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.password);
        }
        [Test]
        public void Should_have_error_when_Password_MaximumLength()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste12345678909" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.password);
        }
        [Test]
        public void Is_Valid_when_Password()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste1234567" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.password);
        }

        [Test]
        public void Should_have_error_when_postal_code_is_validate()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "036070909"};
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.postal_code);
        }
        [Test]
        public void Should_have_error_when_postal_code_is_validate_2()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "03607009P" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.postal_code);
        }
        [Test]
        public void Is_Valid_when_postal_code()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "03607060" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.postal_code);
        }
        [Test]
        public void Is_Valid_when_postal_code_2()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "03607-060" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.postal_code);
        }

        [Test]
        public void Should_have_error_when_phone_number_is_validate()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "1194525998545" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.postal_code);
        }
        [Test]
        public void Should_have_error_when_phone_number_is_validate_2()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "119452599" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.postal_code);
        }

        [Test]
        public void Is_Valid_when_phone_number()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "11945259985" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.phone_number);
        }
        [Test]
        public void Is_Valid_when_phone_number_2()
        {
            var model = new AccountInput { name = "Teste", tax_id = "12345678909", password = "Teste123456", postal_code = "1120820766" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.phone_number);
        }
    }
}
