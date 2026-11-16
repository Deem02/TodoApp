using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    public class HistoryChangeConfiguration : IEntityTypeConfiguration<HistoryChange>
    {
        public void Configure(EntityTypeBuilder<HistoryChange> builder)
        {
            builder.ToTable("HistoryChanges");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FieldName).HasMaxLength(50);


            builder.HasOne(c => c.HistoryEvent)
                   .WithMany(e => e.Changes)
                   .HasForeignKey(c => c.HistoryEventId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
 
}
