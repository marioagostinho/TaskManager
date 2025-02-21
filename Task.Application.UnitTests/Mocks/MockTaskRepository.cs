using Default = System.Threading.Tasks;
using Moq;
using Task.Domain.Repositories;
using Entity = Task.Domain.Entities;
using Task.Domain.Enums;

namespace Task.Application.UnitTests.Mocks
{
    public class MockTaskRepository
    {
        public static Mock<ITaskRepository> GetMockTaskRepository()
        {
            var tasks = new List<Entity.Task>
            {
                new Entity.Task
                {
                    Id = Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"),
                    Title = "Task 1",
                    Description = "Description 1",
                    Status = ETaskStatus.New,
                    DateDelivery = default,
                    DateCreated = default,
                    DateModified = default
                },
                new Entity.Task
                {
                    Id = Guid.Parse("ac3e602b-75ae-499d-9c75-29fbdfeb0f29"),
                    Title = "Task 2",
                    Description = "Description 2",
                    Status = ETaskStatus.InProgress,
                    DateDelivery = default,
                    DateCreated = default,
                    DateModified = default
                },
                new Entity.Task
                {
                    Id = Guid.Parse("5e35d8ac-4178-473a-89b5-b99c1746fd7c"),
                    Title = "Task 3",
                    Description = "Description 3",
                    Status = ETaskStatus.Review,
                    DateDelivery = default,
                    DateCreated = default,
                    DateModified = default

                },
                new Entity.Task
                {
                    Id = Guid.Parse("054e67b7-bb8f-4cd6-b9af-79047ea808ac"),
                    Title = "Task 4",
                    Description = "Description 4",
                    Status = ETaskStatus.Completed,
                    DateDelivery = default,
                    DateCreated = default,
                    DateModified = default
                }
            };

            var mock = new Mock<ITaskRepository>();

            // Setup for GetAllAsync
            mock.Setup(r => r.GetAllAsync(It.IsAny<bool>())).ReturnsAsync(tasks);

            // Setup for GetByIdAsync
            mock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((Guid id, bool track) => tasks.FirstOrDefault(a => a.Id == id));

            // Setup for CreateAsync
            mock.Setup(r => r.CreateAsync(It.IsAny<Entity.Task>()))
                .Returns((Entity.Task task) =>
                {
                    task.Id = Guid.NewGuid();
                    tasks.Add(task);
                    return Default.Task.FromResult(task.Id);
                });

            // Setup for UpdateAsync
            mock.Setup(r => r.UpdateAsync(It.IsAny<Entity.Task>()))
                .ReturnsAsync((Entity.Task task) =>
                {
                    var index = tasks.FindIndex(t => t.Id == task.Id);
                    if (index != -1)
                    {
                        tasks[index] = task;
                        return true;
                    }
                    return false;
                });

            // Setup for DeleteAsync
            mock.Setup(r => r.DeleteAsync(It.IsAny<Entity.Task>()))
                .ReturnsAsync((Entity.Task task) =>
                {
                    var taskToRemove = tasks.FirstOrDefault(t => t.Id == task.Id);

                    if (taskToRemove != null)
                    {
                        tasks.Remove(taskToRemove);
                        return true;
                    }
                    return false;
                });

            return mock;
        }
    }
}
