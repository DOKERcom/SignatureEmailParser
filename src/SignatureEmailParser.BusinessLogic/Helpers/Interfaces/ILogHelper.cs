using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Helpers.Interfaces
{
    public interface ILogHelper
    {
        Task LogSuccessRequest(IHeaderDictionary headers, string source, string message = null);
        
        Task LogErrorRequest(IHeaderDictionary headers, string source, string message = null);
        
        Task LogSevereRequest(IHeaderDictionary headers, string source, string message = null);

        Task LogSuccessRequest(IQueryCollection queryCollection, string source, string message = null);

        Task LogErrorRequest(IQueryCollection queryCollection, string source, string message = null);

        Task LogServerRequest(IQueryCollection queryCollection, string source, string message = null);

        Task<long> GetRequestsCountByLicenseId(string licenseId, string source = null);

        Task<long> GetRequestsCountByDate(DateTime date, string licenseId, string source = null);
    }
}
