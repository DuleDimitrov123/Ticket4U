using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shows.Infrastructure.Persistance;

namespace Shows.IntegrationTests.Base;

public static class ShowsApiFactoryExtensions
{
    public static void AddTestDbContext(this IServiceCollection services, string connectionString)
    {
        var dbContextOptionsDescriptor = services.SingleOrDefault
            (d => d.ServiceType == typeof(DbContextOptions<ShowsDbContext>));

        if (dbContextOptionsDescriptor != null)
        {
            services.Remove(dbContextOptionsDescriptor);
        }

        services.AddDbContext<ShowsDbContext>(options =>
        {
            var connString = connectionString + ";TrustServerCertificate=True"; // EF 7
            options.UseSqlServer(connString);
        });
    }
}