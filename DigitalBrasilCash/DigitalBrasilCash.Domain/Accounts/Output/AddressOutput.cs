using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Accounts.Output
{
    public class AddressOutput
    {
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }
}
