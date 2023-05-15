using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<List<Company>> GetByNames(string[] names)
        {
            List<Company> result = await _context
             .Companies
             .Where(x => names.Contains(x.Name))
             .ToListAsync();

            return result;
        }

        public async Task<Company> GetByName(string name)
        {
            Company result = await _context
             .Companies
             .Where(x => name == x.Name)
             .FirstOrDefaultAsync();

            return result;
        }
    } 
}  
