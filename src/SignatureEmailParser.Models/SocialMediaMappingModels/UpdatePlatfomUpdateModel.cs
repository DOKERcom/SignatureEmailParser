using SignatureEmailParser.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.SocialMediaMappingModels
{
    public class UpdatePlatfomUpdateModel
    {
        public List<PlatfomUpdateModel> PlatfomUpdateModel { get; set; }

        public UpdatePlatfomUpdateModel()
        {
            PlatfomUpdateModel = new List<PlatfomUpdateModel>();
        }
    }

    public class PlatfomUpdateModel
    {
        public string Item { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}
