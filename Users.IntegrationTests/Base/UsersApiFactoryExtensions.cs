using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Identity;

namespace Users.IntegrationTests.Base;

public static class UsersApiFactoryExtensions
{
    public static void AddTestDbContext(this IServiceCollection services, string connectionString)
    {
        var dbContextOptionsDescriptor = services.SingleOrDefault
            (d => d.ServiceType == typeof(DbContextOptions<UsersIdentityDbContext>));

        if (dbContextOptionsDescriptor != null)
        {
            services.Remove(dbContextOptionsDescriptor);
        }

        services.AddDbContext<UsersIdentityDbContext>(options =>
        {
            var connString = connectionString + ";TrustServerCertificate=True"; // EF 7
            options.UseSqlServer(connString);
        });
    }
}
