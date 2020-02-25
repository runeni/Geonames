using System;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace Geonames.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public long IsoNumeric { get; set; }
        public string Rips { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public long Area { get; set; }
        public long Population { get; set; }
        public string Continent { get; set; }
        public string Tld { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Phone { get; set; }
        public string PostalCodeFormat { get; set; }
        public string PostalCodeRegex { get; set; }
        public string Languages { get; set; }
        public long GeonameId { get; set; }
        public string Neighbours { get; set; }
        public string EquivalentFipsCode { get; set; }
    }
}