using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<long> GetRequestsCountByDate(DateTime date, string licenseId, string source = null)
        {
            long count = await _context.Logs.Where(log => log.CreatedAt.Date == date.Date && log.LicenseId == licenseId && log.Source == source).LongCountAsync();

            return count;
        }

        public async Task<long> GetRequestsCountByLicenseId(string licenseId, string source = null)
        {
            long count = await _context.Logs.Where(log => log.LicenseId == licenseId && (log.Source == null || log.Source == source)).LongCountAsync();

            return count;
        }
    }
}
