using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity = Task.Domain.Entities;

namespace Task.Domain.Repositories
{
    public interface ITaskRepository : IBaseRepository<Entity.Task>
    {
    }
}
