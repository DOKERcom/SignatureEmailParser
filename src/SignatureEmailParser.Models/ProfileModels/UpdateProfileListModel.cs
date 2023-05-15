using SignatureEmailParser.EFCore.Enums;
using System.Collections.Generic;

namespace SignatureEmailParser.Models.ProfileModels
{
    public class UpdateProfileListModel
    {
        public List<ProfileLicenseModel> ProfileLicenseModels { get; set; }
        public UpdateProfileListModel()
        {
            ProfileLicenseModels = new List<ProfileLicenseModel>();
        }
    }

    public class ProfileLicenseModel
    {
        public string Email { get; set; }
        public string LicenseId { get; set; }
        public StripeStatusType StripeStatusType { get; set; }
    }
}
