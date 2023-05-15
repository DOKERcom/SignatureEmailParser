using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class TemplateRespondsDailyStatisticRepository : BaseRepository<TempalateAutoRespondStatistic>, ITemplateRespondsDailyStatisticRepository
    {
        public TemplateRespondsDailyStatisticRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<TempalateAutoRespondStatistic>> GetByDate(DateTime date)
        {
            List<TempalateAutoRespondStatistic> result = await _dbSet
                .Where(x => x.CreatedAt.Date == date.Date)
                .ToListAsync();

            return result;
        }
    } 
}  
