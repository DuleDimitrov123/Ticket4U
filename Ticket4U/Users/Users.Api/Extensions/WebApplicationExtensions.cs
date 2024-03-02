using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Identity;

namespace Users.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void AddDBMigrations(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<UsersIdentityDbContext>();
            try
            {
                context.Database.EnsureDeleted();//DELETE IN PROD!
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not run migrations: {ex.Message}");
            }
        }
    }

    public async static Task AddUserRoles(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
