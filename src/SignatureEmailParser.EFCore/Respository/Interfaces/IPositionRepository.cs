using SignatureEmailParser.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task<List<Position>> FindByPositionAsync(List<long> positionId);
        Task<List<Position>> FindByNameAsync(string name, bool exact, long positionId = 0);
        Task<Position> FindById(long id);
        Task<Position> AddRangeAsync(IEnumerable<Position> positions);
        Task<Position> GetLastIdAsync();
    }
}
