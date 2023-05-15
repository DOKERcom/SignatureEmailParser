using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.Models.SocialMediaMappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public static class ProfileHelper
    {
        public static List<ProfileFromAppModel> ProfileCleaner(List<ProfileFromAppModel> profiles)
        {
            List<ProfileFromAppModel> profileCleaned = new List<ProfileFromAppModel>();

            foreach (ProfileFromAppModel profile in profiles)
            {
                if (!string.IsNullOrEmpty(profile.Company) ? profile.Company.ToLower() == ProfileItemStatus.DEFAULT_COMPANY_INVITE.ToLower() : true)
                {
                    profile.Company = ProfileItemStatus.NOT_AVAILABLE;
                }
                else
                {
                    profile.Company = profile.Company.TrimStart().TrimEnd();
                }

                if (!string.IsNullOrEmpty(profile.PositionTitle) ? profile.PositionTitle.ToLower() == ProfileItemStatus.DEFAULT_POSITION_INVITE.ToLower() : true)
                    profile.PositionTitle = ProfileItemStatus.NOT_AVAILABLE; 

                if (profile.ProfileUrl.Contains("?"))
                {
                    profile.ProfileUrl = profile.ProfileUrl.Split('?').FirstOrDefault();
                }
                else if (profile.ProfileUrl.Contains('/'))
                {
                    profile.ProfileUrl = profile.ProfileUrl.TrimEnd('/');
                }

                profileCleaned.Add(profile);
            }

            return profileCleaned;
        }

    }
}
