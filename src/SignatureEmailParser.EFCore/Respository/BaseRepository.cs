using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> Create(TEntity item)
        {
            await _dbSet.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task CreateRange(List<TEntity> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> result = await _dbSet
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<TEntity> GetById(long id)
        {
            TEntity result = await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<TEntity>> GetByIds(long[] ids)
        {
            List<TEntity> result = await _dbSet
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
            return result;
        }

        public async Task<bool> Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateRange(List<TEntity> items)
        {
            if (items is null || !items.Any())
            {
                return 0;
            }

            _dbSet.UpdateRange(items);

            return await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(List<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> CreateRangeWihtIds(List<TEntity> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            return items;
        }
    }
}
