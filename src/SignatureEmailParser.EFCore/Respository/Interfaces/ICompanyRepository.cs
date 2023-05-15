using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<List<Company>> GetByNames(string[] names);

        Task<Company> GetByName(string name);
    }
}
