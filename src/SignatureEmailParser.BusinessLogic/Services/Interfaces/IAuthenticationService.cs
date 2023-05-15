using SignatureEmailParser.Models.Enums;
using SignatureEmailParser.Models.Requests;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> HasPermissionsAsync(IHeaderDictionary headers, string source, bool ignoreRequestLimits = false);
        Task<ResponseModel> HasPermissionsAsync(IQueryCollection queryCollection, string source, bool ignoreRequestLimits = false);
    }
}
