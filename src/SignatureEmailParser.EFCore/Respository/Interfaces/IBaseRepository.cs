using SignatureEmailParser.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(long id);
        Task<List<TEntity>> GetByIds(long[] ids);
        Task <bool> Create(TEntity item);
        Task <bool> Update(TEntity item);
        Task Remove(TEntity item);
        Task CreateRange(List<TEntity> items);
        Task<List<TEntity>> CreateRangeWihtIds (List<TEntity> items);
        Task RemoveRange(List<TEntity> items);
        Task<int> UpdateRange(List<TEntity> items);
    }
}
