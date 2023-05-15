using System.ComponentModel;

namespace SignatureEmailParser.EFCore.Enums
{
    public enum SocialMediaResultStatusType : int
    {
        [Description("Valid")]
        Valid = 0,

        [Description("Invalid")]
        Invalid = 1
    }
}
