using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class LocationModel
    {
        public CountryModel Country { get; set; }

        public RegionModel Region { get; set; }

        public CityModel City { get; set; }
    }
}
