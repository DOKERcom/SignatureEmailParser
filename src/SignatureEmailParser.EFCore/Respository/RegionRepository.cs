using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class RegionRepository : BaseRepository<City>, IRegionRepository
    {
        public RegionRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Regions>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<List<Regions>> GetAllNewByLatestIdAsync(long latestId)
        {
            return await _context.Regions.Where(city => city.Id > latestId).ToListAsync();
        }
    }
}
