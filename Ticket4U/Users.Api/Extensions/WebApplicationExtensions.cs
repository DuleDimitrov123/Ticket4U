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
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not run migrations: {ex.Message}");
            }
        }
    }
}
