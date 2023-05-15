using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class RegionModel
    {
        public long Id { get; set; }

        public long CountryId { get; set; }

        public string RegionName { get; set; }
    }
}
