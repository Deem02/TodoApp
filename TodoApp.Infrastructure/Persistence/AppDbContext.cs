using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<HistoryEvent> HistoryEvents => Set<HistoryEvent>();
        public DbSet<HistoryChange> HistoryChanges => Set<HistoryChange>();
        public DbSet<RequestLog> RequestLogs => Set<RequestLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
