using MediatR;
using Task.Domain.Enums;

namespace Task.Application.Features.Task.Commands.CreateTask
{
    public record CreateTaskCommand(string Title, string Description, ETaskStatus Status) : IRequest<Guid>;
}
