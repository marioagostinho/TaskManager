using Task.Domain.Enums;

namespace Task.Application.Dtos
{
    public record TaskDto(Guid Id, string Title, string Description, ETaskStatus Status, DateTime DateDelivery, DateTime DateCreated, DateTime DateModified);
}
