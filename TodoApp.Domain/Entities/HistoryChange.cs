namespace TodoApp.Domain.Entities
{
    public class HistoryChange
    {
        public int Id { get; set; }
        public int HistoryEventId { get; set; }
        public string FieldName { get; set; } = string.Empty; 
        public string? OldValue { get; set; } 
        public string? NewValue { get; set; }

        public HistoryEvent HistoryEvent { get; set; } = null!;
    }
}
