using SignatureEmailParser.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface IPositionService
    {
        Task<List<Position>> GetAllNewByLatestIdAsync(long latestId);
    }
}
