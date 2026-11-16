using TodoApp.Domain.Enums;
namespace TodoApp.Domain.Entities
{
    public class HistoryEvent
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int ChangedByUserId { get; set; }

        public HistoryAction Action { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public TaskItem Task { get; set; } = null!;

        public User ChangedByUser { get; set; } = null!;
        public ICollection<HistoryChange> Changes { get; set; } = [];


    }
}
