using SignatureEmailParser.EFCore.Entities;
using System;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface ILogRepository : IBaseRepository<Log>
    {
        Task<long> GetRequestsCountByLicenseId(string licenseId, string source = null);

        Task<long> GetRequestsCountByDate(DateTime date, string licenseId, string source = null);
    }
}
