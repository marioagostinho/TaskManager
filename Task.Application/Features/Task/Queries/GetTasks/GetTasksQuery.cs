using MediatR;
using Task.Application.Dtos;

namespace Task.Application.Features.Task.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<IReadOnlyList<TaskListDto>>;
}
