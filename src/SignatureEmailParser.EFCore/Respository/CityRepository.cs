using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<City>> GetAllNewByLatestIdAsync(long latestId)
        {
            return await _context.City.Where(city => city.Id > latestId).ToListAsync();
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<List<City>> FindByNameAsync(string name)
        {
            return await _context.City.Where(city => EF.Functions.Like(city.Name, $"%{name}%")).ToListAsync();
        }
    }
}
