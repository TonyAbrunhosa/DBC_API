using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Accounts.Output
{
    public class AccountOutput
    {
        public string name { get; set; }
        public string tax_id { get; set; }
        public string phone_number { get; set; }
        public string postal_code { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public AddressOutput address { get; set; }
    }
}
