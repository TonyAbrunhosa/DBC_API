using System;

namespace DigitalBrasilCash.Domain.Accounts.Output
{
    public class AccountOutput
    {
        public AccountOutput(int id_account, string name, string tax_id, string phone_number, string postal_code, string status, DateTime created_at)
        {
            Id_account = id_account;
            Name = name;
            Tax_id = tax_id;
            Phone_number = phone_number;
            Postal_code = postal_code;
            Status = status;
            Created_at = created_at;
        }

        public int Id_account { get; private set; }
        public string Name { get; private set; }
        public string Tax_id { get; private set; }
        public string Phone_number { get; private set; }
        public string Postal_code { get; private set; }
        public string Status { get; private set; }
        public DateTime Created_at { get; private set; }
        public AddressOutput Address { get; private set; }

        public void AddAdress(string street, string district, string city, string state)
        {
            Address = new AddressOutput(street, district, city, state);
        }
    }
}
