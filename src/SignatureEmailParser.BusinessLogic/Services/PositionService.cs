using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<List<Position>> GetAllNewByLatestIdAsync(long latestId)
        {
            return await _positionRepository.GetAllAsync();
        }
    }
}
