using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class EmailParseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Mobile { get; set; }

        public string Telephone { get; set; }

        public string LinkedIn { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public long PositionId { get; set; }

        public string Country { get; set; }

        public long CountryId { get; set; }

        public string Region { get; set; }
        
        public long RegionId { get; set; }

        public string City { get; set; }

        public long CityId { get; set; }

        public string Postcode { get; set; }
        
        public int PartsAvailable { get; set; }

        public int PartsFound { get; set; }

        public int PartsFoundPercent { get; set; }

    }
}
