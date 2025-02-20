using Task.Domain.Enums;

namespace Task.Application.Dtos
{
    public record TaskListDto(Guid Id, string Title, ETaskStatus Status);
}
