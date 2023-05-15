using System.ComponentModel;

namespace SignatureEmailParser.Models.Enums
{
    public enum DataSourceType : int
    {
        [Description("NA")]
        NA = 0,

        [Description("LinkedIn App")]
        LinkedInApp = 1,

        [Description("SNOV.IO")]
        SnovIO = 2,

        [Description("Nymeria")]
        Nymeria = 3,

        [Description("LinkedIn Data")]
        LinkedInData = 4,

        [Description("Facebook Data")]
        FacebookData = 5,

        [Description("Twitter Data")]
        TwitterData = 6,

        [Description("Email Signature")]
        EmailSignature = 7
    }
}
