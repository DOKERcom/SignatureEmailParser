using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class PositionRepository : BaseRepository<City>, IPositionRepository
    {
        public PositionRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<Position> AddRangeAsync(IEnumerable<Position> positions)
        {
            throw new System.NotImplementedException();
        }

        public Task<Position> FindById(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Position>> FindByNameAsync(string name, bool exact, long positionId = 0)
        {
            return await _context.Positions.Where(i => i.Name.Equals(name))
                     .ToListAsync();
        }

        public Task<List<Position>> FindByPositionAsync(List<long> positionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Regions>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public Task<Position> GetLastIdAsync()
        {
            throw new System.NotImplementedException();
        }

        Task<List<Position>> IPositionRepository.GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
