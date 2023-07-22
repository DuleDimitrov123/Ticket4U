using Microsoft.EntityFrameworkCore;
using Shows.Infrastructure.Persistance;

namespace Shows.Api.Helpers;

public static class WebApplicationExtensions
{
    public static void AddDBMigrations(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ShowsDbContext>();
            context.Database.Migrate();
        }
    }
}
