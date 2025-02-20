using Microsoft.EntityFrameworkCore;
using Task.Domain.Enums;
using Task.Domain.Repositories;
using Task.Persistence.Contexts;
using Entity = Task.Domain.Entities;

namespace Task.Persistence.Repositories
{
    public class TaskRepository : BaseRepository<Entity.Task>, ITaskRepository
    {
        public TaskRepository(TaskDbContext context) : base(context)
        {
        }

        public override async Task<bool> UpdateAsync(Entity.Task entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(x => x.DateCreated).IsModified = false;

            if (entity.Status == ETaskStatus.Completed)
            {
                entity.DateDelivery = DateTime.UtcNow;
            }
            else
            {
                _context.Entry(entity).Property(x => x.DateDelivery).IsModified = false;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
