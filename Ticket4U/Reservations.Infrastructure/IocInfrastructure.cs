using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.Contracts.Persistance;
using Reservations.Infrastructure.Persistance.Repositories;

namespace Reservations.Infrastructure;

public static class IocInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReservationsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ReservationsConnectionString"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddCap(options =>
        {
            options.UseEntityFramework<ReservationsDbContext>();

            options.UseRabbitMQ(configuration.GetSection("EventBusSettings:Host").Value!);
        });

        return services;
    }
}
