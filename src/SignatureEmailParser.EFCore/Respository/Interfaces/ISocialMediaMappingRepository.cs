using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignatureEmailParser.Models;
using SignatureEmailParser.Models.SocialMediaMappingModels;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface ISocialMediaMappingRepository : IBaseRepository<SocialMediaMapping>
    {
        Task<List<SocialMediaMapping>> GetByPrivateEmails(string[] emails);

        Task<List<SocialMediaMapping>> GetByEmails(string[] emails);

        Task<SocialMediaMapping> GetByAnyIdentifiableIds(ProfileFromAppModel profileFromAppModel);

        Task<List<SocialMediaMapping>> GetByLinkedInIds(string[] ids);

        Task<List<SocialMediaMapping>> GetByTwitterIds(string[] ids);

        Task<List<SocialMediaMapping>> GetByFacebookIds(string[] ids);

        Task<List<SocialMediaMapping>> GetByLinkedInUrl(string[] url);

        Task<List<SocialMediaMapping>> GetByMobile(string[] mobileNum);

        Task<List<SocialMediaMapping>> GetByPrivatePhone(string[] phoneNum);

        Task<List<SocialMediaMapping>> ExecuteQuery(string query);
        
        Task CreateOrUpdate(SocialMediaMapping socialMediaMapping);

        Task<List<string>> GetInferredSalaryFiltersAsync();
    }
}
