using System.Collections.Concurrent;
using System.Collections.Generic;
using SignatureEmailParser.Models.Enums;

namespace SignatureEmailParser.Models.SocialMediaMappingModels
{
    public class GetSocialMediaMappingsByFilterRequestModel
    {
        public ConcurrentDictionary<SocialMediaMappingsColumns, string> ColumnValueSearchDictionary { get; set; }

        public List<string> ProfilesToExclude { get; set; }

        public int? AgeStart { get; set; }

        public int? AgeEnd { get; set; }

        public int? SalaryStart { get; set; }

        public int? SalaryEnd { get; set; }

        public int? ExperienceStart { get; set; }

        public int? ExperienceEnd { get; set; }

        public bool IsTwitter { get; set; }

        public bool IsFacebook { get; set; }

        public bool IsCurrentCompanyOnly { get; set; }
        
        public int Take { get; set; }

        public int Skip { get; set; }

        public EmailWorkOrPersonalOptionsType EmailWorkOrPersonalOptionsType { get; set; }

        public PhoneOptionsType PhoneOptionsType { get; set; }
    }
}