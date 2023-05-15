using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface IIndustryRepository : IBaseRepository<Industry>
    {
        Task<List<Industry>> GetByNames(string[] names);

        Task<Industry> GetByName(string name);
    }
}
