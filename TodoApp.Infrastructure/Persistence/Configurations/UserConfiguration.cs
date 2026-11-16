using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName).HasMaxLength(150);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.PasswordHash).HasMaxLength(256);
            builder.Property(u => u.Role).HasDefaultValue(UserRole.User);
            
            builder.HasIndex(u => u.Email).IsUnique();

        }
    }
}
