using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.Models.Enums
{
    public enum RequestType : int
    {
        [Description("NA")]
        NA = 0,

        [Description("HttpTriggerGetProfilesWithMissingInfo")]
        HttpTriggerGetProfilesWithMissingInfo = 1,

        [Description("HttpTriggerGetSocialMediaMappingsByFilter")]
        HttpTriggerGetSocialMediaMappingsByFilter = 2,

        [Description("HttpTriggerImportProfilesFromLinkedInApp")]
        HttpTriggerImportProfilesFromLinkedInApp = 3,

        [Description("HttpTriggerSearchSocialMediaMapping")]
        HttpTriggerSearchSocialMediaMapping = 4,

        [Description("HttpTriggerStoreSocialMediaProfilesFunction")]
        HttpTriggerStoreSocialMediaProfilesFunction = 5,

        [Description("HttpTriggerUpdateProfilesFromSocialMediaFunction")]
        HttpTriggerUpdateProfilesFromSocialMediaFunction = 6
    }
}