using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class CityModel
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string CityName { get; set; }
        public long? RegionId { get; set; }
        public long FId { get; set; }
    }
}
