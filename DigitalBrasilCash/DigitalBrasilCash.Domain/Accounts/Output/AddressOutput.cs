using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Domain.Accounts.Output
{
    public class AddressOutput
    {
        public AddressOutput(string street, string district, string city, string state)
        {
            Street = street;
            District = district;
            City = city;
            State = state;
        }

        public string Street { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
    }
}
