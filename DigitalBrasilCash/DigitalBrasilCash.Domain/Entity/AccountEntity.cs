using Dapper.Contrib.Extensions;
using System;

namespace DigitalBrasilCash.Domain.Entity
{
    [Table("Account")]
    public class AccountEntity
    {
        [Key]
        public int id_account { get; set; }
        public string name { get; set; }
        public string tax_id { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string postal_code { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
    }
}
