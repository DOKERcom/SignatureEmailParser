using System.Collections.Generic;

namespace SignatureEmailParser.Models.SocialMediaMappingModels
{
    public class FreeTextSearchRequestModel
    {
        public string Fields { get; set; }
        public string Values { get; set; }
        public List<string> IgnoreIds { get; set; }
        public int TakeNumber { get; set; }

        public FreeTextSearchRequestModel()
        {
            IgnoreIds = new List<string>();
        }
    }
}
