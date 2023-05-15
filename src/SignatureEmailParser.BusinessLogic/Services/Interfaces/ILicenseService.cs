using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface ILicenseService
    {
        Task UpdateProfilesLicenseAsync(string requestBody);
    }
}
