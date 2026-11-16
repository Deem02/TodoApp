using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    public class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
    {
        public void Configure(EntityTypeBuilder<RequestLog> builder)
        {
            builder.ToTable("RequestLogs");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Method).HasMaxLength(10);
            builder.Property(l => l.IpAddress).HasMaxLength(45);

            builder.HasOne(l => l.User)
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(l => l.CreatedAt);
            builder.HasIndex(l => new { l.UserId, l.CreatedAt });
        }


    }
}
