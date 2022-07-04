using Dapper.Contrib.Extensions;
using System;

namespace DigitalBrasilCash.Domain.Entity
{
    [Table("ACCOUNT")]
    public class AccountEntity
    {
        public AccountEntity(string name, string tax_id, string password, string phone_number, string postal_code, int id_account = 0)
        {
            Id_account = id_account;
            Name = name;
            Tax_id = tax_id;
            Password = password;
            Phone_number = phone_number.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            Postal_code = postal_code.Replace("-", "").Replace(" ", "");
            Status = string.IsNullOrEmpty(postal_code) ? "pending" : "approved";
            Created_at = DateTime.Now;
        }

        [Key]
        public int Id_account { get; set; }
        public string Name { get; set; }
        public string Tax_id { get; set; }
        public string Password { get; set; }
        public string Phone_number { get; set; }
        public string Postal_code { get; set; }
        public string Status { get; set; }
        public DateTime Created_at { get; set; }
    }
}
