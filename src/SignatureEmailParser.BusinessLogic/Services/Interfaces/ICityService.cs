using System.Collections.Generic;
using System.Threading.Tasks;
using SignatureEmailParser.EFCore.Entities;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetAllNewByLatestIdAsync(long latestId);
    }
}