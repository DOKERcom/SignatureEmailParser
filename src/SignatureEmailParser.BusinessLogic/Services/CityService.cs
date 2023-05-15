using System.Collections.Generic;
using System.Threading.Tasks;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> GetAllNewByLatestIdAsync(long latestId)
        {
            return await _cityRepository.GetAllNewByLatestIdAsync(latestId);
        }
    }
}