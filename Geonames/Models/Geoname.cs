namespace Geonames.Models
{
    public class Geoname
    {
        public int Id { get; set; }
        public int GeonameId { get; set; }
        public string Name { get; set; }
        public string AsciiName { get; set; }
        public string AlternateNames { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public char? FeatureClass { get; set; }
        public string FeatureCode { get; set; }
        public string CountryCode { get; set; }
        public string Cc2 { get; set; }
        public string Admin1Code { get; set; }
        public string Admin2Code { get; set; }
        public string Admin3Code { get; set; }
        public string Admin4Code { get; set; }
        public int? Population { get; set; }
        public int? Elevation { get; set; }
        public int? Dem { get; set; }
        public string Timezone { get; set; }
        public Geoname Parent { get; set; }
    }
}