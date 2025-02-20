using MediatR;
using Task.Application.Dtos;

namespace Task.Application.Features.Task.Queries.GetTaskDetails
{
    public record GetTaskDetailsQuery(Guid Id) : IRequest<TaskDto>;
}
