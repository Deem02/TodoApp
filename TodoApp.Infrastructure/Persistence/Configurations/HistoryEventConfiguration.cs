using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    public class HistoryEventConfiguration : IEntityTypeConfiguration<HistoryEvent>
    {
        public void Configure(EntityTypeBuilder<HistoryEvent> builder)
        {
            builder.ToTable("HistoryEvents");
            builder.HasKey(e => e.Id);


            builder.HasOne(e => e.Task)
                   .WithMany(t => t.HistoryEvents)
                   .HasForeignKey(e => e.TaskId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ChangedByUser)
                   .WithMany()
                   .HasForeignKey(e => e.ChangedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new { e.TaskId, e.ChangedAt });
        }
    }
}
