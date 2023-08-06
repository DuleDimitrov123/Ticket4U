using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.Contracts.Outbox;
using Reservations.Application.Contracts.Persistance;
using Reservations.Infrastructure.Outbox;
using Reservations.Infrastructure.Persistance.Repositories;
using Shared.Infrastructure.Authentication;

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

        services.AddScoped<IShowRepository, ShowRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        var capOptionsConstants = new CapOptionsConstants();
        configuration.Bind("CapOptionsConstants", capOptionsConstants);

        services.AddCap(options =>
        {
            options.FailedRetryCount = capOptionsConstants.FailedRetryCount;

            options.UseEntityFramework<ReservationsDbContext>();

            options.UseRabbitMQ(configuration.GetSection("EventBusSettings:Host").Value!);
        });

        services.AddScoped<IReservationPublisher, ReservationPublisher>();

        services.AddCustomAuthentication(configuration);

        return services;
    }
}
