using SignatureEmailParser.EFCore.Enums;

namespace SignatureEmailParser.EFCore.Entities
{
    public class StripeIdentity : BaseEntity
    {
        public string LicenseId { get; set; }

        public string Email { get; set; }

        public StripeStatusType StripeStatusType { get; set; }
    }
}
