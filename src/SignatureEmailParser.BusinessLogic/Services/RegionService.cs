using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<List<Regions>> GetAllNewByLatestIdAsync(long latestId)
        {
            return await _regionRepository.GetAllNewByLatestIdAsync(latestId);
        }
    }
}
