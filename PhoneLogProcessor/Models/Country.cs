using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogProcessor.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public Country(int countryId, string name)
        {
            CountryId = countryId;
            Name = name;
        }
    }
}
