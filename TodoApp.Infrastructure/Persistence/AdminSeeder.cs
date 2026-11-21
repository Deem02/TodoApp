using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;


namespace TodoApp.Infrastructure.Persistence
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminUserAsync(AppDbContext context, IConfiguration configuration)
        {
            
            if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                return; // Admin user already exists
            }
            var admin = new User
            {
                FullName = configuration["SeedAdmin:FullName"]!,
                Email = configuration["SeedAdmin:Email"]!.ToLowerInvariant(),
                Role = UserRole.Admin
            };

            admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, configuration["SeedAdmin:Password"]!);

            context.Users.Add(admin);
            await context.SaveChangesAsync();


        }
    }
}
