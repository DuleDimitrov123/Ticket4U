using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Users.Infrastructure.Identity;

public class UsersIdentityDbContextFactory : IDesignTimeDbContextFactory<UsersIdentityDbContext>
{
    public UsersIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UsersIdentityDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("UsersConnectionString"));

        return new UsersIdentityDbContext(optionsBuilder.Options);
    }
}
