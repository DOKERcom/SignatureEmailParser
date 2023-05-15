using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Regions>> GetAllAsync();
        Task<List<Regions>> GetAllNewByLatestIdAsync(long latestId);
    }
}
