using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Domain.Repositories;
using Task.Persistence.Contexts;
using Task.Persistence.Repositories;

namespace Task.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TaskConnectionString"));
            });

            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
