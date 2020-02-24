using System;

namespace Geonames.Models
{
    public class FeatureClassification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ClassCode { get; set; }
        public string Lang { get; set; }
    }
}