using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Accounts.Input
{
    public class AccountInput
    {
        public string name { get; set; }
        public string tax_id { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string postal_code { get; set; }
    }
}
