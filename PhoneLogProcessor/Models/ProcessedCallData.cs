namespace PhoneLogProcessor.Models
{
    public class ProcessedCallData
    {
        public string CountryName { get; set; }
        public string DistrictName { get; set; }
        public int CountryId { get; set; }
        public int DistrictId { get; set; }
        public int CallDuring { get; set; }

        public override string ToString() => $"{CountryName}\t{DistrictName}\t{CountryId}\t{DistrictId}\t{CallDuring}";
    }
}
