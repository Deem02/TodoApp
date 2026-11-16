using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).HasMaxLength(200);
            builder.Property(t => t.Description).HasMaxLength(2000);
            builder.Property(t => t.IsDeleted).HasDefaultValue(false);


            builder.HasOne(t => t.User)
                   .WithMany(u => u.Tasks)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.UserId, t.IsDeleted, t.Status });
            builder.HasIndex(t => new { t.UserId, t.DueDate });
            builder.HasIndex(t => new { t.UserId, t.CompletedAt });
        }
    }
}
