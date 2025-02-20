using MediatR;

namespace Task.Application.Features.Task.Commands.DeleteTask
{
    public record DeleteTaskCommand(Guid Id) : IRequest<bool>;
}
