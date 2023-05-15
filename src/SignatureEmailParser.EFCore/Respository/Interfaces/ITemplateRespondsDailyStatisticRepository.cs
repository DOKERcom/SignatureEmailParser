using SignatureEmailParser.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface ITemplateRespondsDailyStatisticRepository : IBaseRepository<TempalateAutoRespondStatistic>
    {
        Task<List<TempalateAutoRespondStatistic>> GetByDate(DateTime date);
    }
}
