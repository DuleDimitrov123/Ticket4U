using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Authentication;
using Shared.Infrastructure.Outbox;
using Shows.Application.Contracts.Outbox;
using Shows.Application.Contracts.Persistance;
using Shows.Infrastructure.Outbox;
using Shows.Infrastructure.Persistance;
using Shows.Infrastructure.Persistance.Repositories;

namespace Shows.Infrastructure;

public static class IocInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShowsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ShowsConnectionString"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IPerformerRepository, PerformerRepository>();
        services.AddScoped<IShowRepository, ShowRepository>();

        //CAP outbox
        var capOptionsConstants = new CapOptionsConstants();
        configuration.Bind("CapOptionsConstants", capOptionsConstants);

        services.AddCap(options =>
        {
            options.FailedRetryCount = capOptionsConstants.FailedRetryCount;

            options.UseEntityFramework<ShowsDbContext>();

            options.UseRabbitMQ(configuration.GetSection("EventBusSettings:Host").Value!);
        });

        services.AddScoped<IShowPublisher, ShowPublisher>();

        services.AddCustomAuthentication(configuration);

        return services;
    }
}
