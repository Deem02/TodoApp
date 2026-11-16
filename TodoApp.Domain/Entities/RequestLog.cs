namespace TodoApp.Domain.Entities
{
    public class RequestLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public int DurationMs { get; set; } 
        public string? IpAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
    }
}
