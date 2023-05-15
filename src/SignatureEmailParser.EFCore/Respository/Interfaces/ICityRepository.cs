using SignatureEmailParser.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllNewByLatestIdAsync(long latestId);

        Task<List<City>> FindByNameAsync(string name);

        Task<List<City>> GetAllAsync();
    }
}
