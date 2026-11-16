using TodoApp.Domain.Enums;

namespace TodoApp.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public User User { get; set; } = null!;

        public ICollection<HistoryEvent> HistoryEvents { get; set; } = [];


    }
}
