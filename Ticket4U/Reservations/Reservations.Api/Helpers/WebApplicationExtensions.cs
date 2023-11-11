using Microsoft.EntityFrameworkCore;
using Reservations.Infrastructure;

namespace Reservations.Api.Helpers;

public static class WebApplicationExtensions
{
    public static void AddDBMigrations(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ReservationsDbContext>();
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
}
