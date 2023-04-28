using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shows.Application.Contracts.Persistance;
using Shows.Infrastructure.Persistance;
using Shows.Infrastructure.Persistance.Repositories;

namespace Shows.Infrastructure;

public static class IocInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ShowsConnectionString"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IPerformerRepository, PerformerRepository>();
        services.AddScoped<IShowRepository, ShowRepository>();

        return services;
    }
}
