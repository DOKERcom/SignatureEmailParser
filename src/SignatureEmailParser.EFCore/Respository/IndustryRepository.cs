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
    public class IndustryRepository : BaseRepository<Industry>, IIndustryRepository
    {
        public IndustryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Industry> GetByName(string name)
        {
            Industry result = await _context
            .Industries
            .Where(x => name.Equals(x.Name))
            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Industry>> GetByNames(string[] names)
        {
            List<Industry> result = await _context
            .Industries
            .Where(x => names.Contains(x.Name))
            .ToListAsync();

            return result;
        }
    }
}
