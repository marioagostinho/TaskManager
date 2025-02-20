using Task.Domain.Enums;

namespace Task.Domain.Entities
{
    public class Task : BaseEntity
    {
        
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ETaskStatus Status { get; set; }
        public DateTime DateDelivery { get; set; }
    }
}
