using SignatureEmailParser.EFCore.Enums;
using System;
using SignatureEmailParser.Models.Enums;

namespace SignatureEmailParser.Models.SocialMediaMappingModels
{
    public class ProfileFromAppModel
    {
        public string ProfileUrl { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public long CompanyId { get; set; }
        public string ProfileLanguage { get; set; }
        public string PositionTitle { get; set; }

        // delete positionTitle above
        public long PositionId { get; set; }

        public string Industry { get; set; }

        // delete industry above
        public long IndustryId { get; set; }

        public string Email { get; set; }

        public string WorkEmail { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }

        public long CityId { get; set;  }
        public string Street { get; set; }
        public string Country { get; set; } // this should be removed once (if) the db schema is updated
        public long CountryId { get; set; }

        public string Postcode { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }

        public string Mobile { get; set; }
        public DateTime WhenUpdated { get; set; }
        public DataSourceType DataSourceType { get; set; }

        public PlatformType PlatformType { get; set; }
        
        public string LastPost { get; set; }
        public string SharedConnections { get; set; }

        public DateTime? LastPostDate { get; set; }

        public string Experience { get; set; }
    }
}
