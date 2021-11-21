namespace PhoneLogProcessor.Models
{
    public class Area
    {
        public int CountryId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }

        public Area(int countryId, int districtId, string name)
        {
            CountryId = countryId;
            DistrictId = districtId;
            Name = name;
        }
    }
}
