using SignatureEmailParser.BusinessLogic.Enums;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.Models.SocialMediaMappingModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface ISocialMediaMappingService
    {
        Task<List<ProfileFromAppModel>> GetProfilesWithMissingInfo(string requestBody);
        Task<ProfileFromAppModel> GetUpdatedProfileSocialMediaByUrl(string url);
        Task<List<ProfileFromAppModel>> GetUpdatedProfilesFromSocialMedia(string requestBody);
        Task<(bool, string)> ImportProfilesFromLinkedInApp(string requestBody);
        Task<List<SocialMediaMapping>> FreeTextSearch(string requestBody);
        Task<List<SocialMediaMapping>> GetByFilterAsync(string requestBody);
        Task<bool> UpdateSocialMediaMappingStatus(string requestBody);
        Task<int> UpdateEmailPhoneStatus(string requestBody);
    }
}
