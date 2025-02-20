using MediatR;
using Task.Domain.Enums;

namespace Task.Application.Features.Task.Commands.UpdateTask
{
    public record UpdateTaskCommand(Guid Id, string Title, string Description, ETaskStatus Status) : IRequest<bool>;
}
