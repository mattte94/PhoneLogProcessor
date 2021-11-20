using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogProcessor.Models
{
    public class Area
    {
        public int DistrictId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public Area(int districtId, int countryId, string name)
        {
            DistrictId = districtId;
            CountryId = countryId;
            Name = name;
        }
    }
}
