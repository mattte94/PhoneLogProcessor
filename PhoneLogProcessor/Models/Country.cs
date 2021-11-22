namespace PhoneLogProcessor.Models
{
    /// <summary>
    /// Ország adatainak tárolására szolgáló osztály
    /// </summary>
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
