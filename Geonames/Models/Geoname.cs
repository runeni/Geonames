using System.ComponentModel;

namespace Geonames.Models
{
    public class Geoname
    {
        public int Id { get; set; }
        [DisplayName("GeoId")]
        public int GeonameId { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        public string AsciiName { get; set; }
        public string AlternateNames { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public char? FeatureClass { get; set; }
        public string FeatureCode { get; set; }
        [DisplayName("Land")]
        public string CountryCode { get; set; }
        public string Cc2 { get; set; }
        public string Admin1Code { get; set; }
        public string Admin2Code { get; set; }
        public string Admin3Code { get; set; }
        public string Admin4Code { get; set; }
        public int? Population { get; set; }
        public int? Elevation { get; set; }
        public int? Dem { get; set; }
        [DisplayName("Tidssone")]
        public string Timezone { get; set; }
        public Geoname Parent { get; set; }
        public Country Country { get; set; }
        public Admin1CodesAscii Admin1CodesAscii { get; set; }
        
        [DisplayName("Type")]
        public FeatureClassification FeatureClassification { get; set; }
    }
}