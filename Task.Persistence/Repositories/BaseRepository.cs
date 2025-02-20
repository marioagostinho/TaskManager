using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task.Domain.Entities;
using Task.Domain.Repositories;
using Task.Persistence.Contexts;

namespace Task.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly TaskDbContext _context;

        public BaseRepository(TaskDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(bool track = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (track == false)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, bool track = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<Guid> CreateAsync(T entity)
        {
            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(x => x.DateCreated).IsModified = false;

            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
