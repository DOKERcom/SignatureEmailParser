using System.ComponentModel;

namespace SignatureEmailParser.EFCore.Enums
{
    public enum StripeStatusType : int
    {
        [Description("OK")]
        OK = 0,

        [Description("Credit card has been declined")]
        Declined = 1,

        [Description("Credit card has expired")]
        Expired = 2
    }
}
