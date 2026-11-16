using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<TaskItem> Tasks { get; }

        DbSet<HistoryEvent> HistoryEvents { get; }

        DbSet<HistoryChange> HistoryChanges { get; }

        DbSet<RequestLog> RequestLogs { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default); 


    }
}
