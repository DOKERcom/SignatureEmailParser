using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface IRegionService
    {
        Task<List<Regions>> GetAllNewByLatestIdAsync(long latestId);
    }
}
