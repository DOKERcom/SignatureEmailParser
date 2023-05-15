using SignatureEmailParser.EFCore.Enums;

namespace SignatureEmailParser.EFCore.Entities
{
    public class Log : BaseEntity
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string Email { get; set; }
        public string LicenseId { get; set; }
        public LogStatusType Status { get; set; }
    }
}
