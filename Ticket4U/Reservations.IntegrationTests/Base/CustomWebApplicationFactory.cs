using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.Infrastructure;
using Reservations.IntegrationTests.Constants;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Base;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    private readonly MsSqlTestcontainer _container =
        new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration
        {
            Database = "ReservationsIntegrationTests",
            Password = "localdevpassword#123",
        })
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTestDbContext(_container.ConnectionString);

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ReservationsDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                context.Database.EnsureCreated();

                try
                {
                    InitializeDbForTestsAsync(sp);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            };
        });
    }

    private async Task InitializeDbForTestsAsync(IServiceProvider serviceProvider)
    {
        var showRepository = serviceProvider.GetRequiredService<IRepository<Show>>();
        var userRepository = serviceProvider.GetRequiredService<IRepository<User>>();
        var reservationRepository = serviceProvider.GetRequiredService<IRepository<Reservation>>();

        var show1 = Show.Create(InstanceConstants.Show1Id, "Show1", DateTime.Now.AddDays(10), 100, InstanceConstants.ExternalShow1Id);
        var user1 = User.Create(InstanceConstants.User1Id, "user1@gmail.com", "user1");
        var reservation1 = Reservation.Create(InstanceConstants.ReservationId1, InstanceConstants.User1Id, InstanceConstants.Show1Id, NumberOfReservations.Create(3));

        await showRepository.Add(show1);
        await userRepository.Add(user1);
        await reservationRepository.Add(reservation1);
    }

    public void CustomConfigureServices(IWebHostBuilder builder, ITestOutputHelper testOutputHelper)
    {
        builder.ConfigureServices(services =>
        {
            // Get service provider.
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
            }
        })
            .UseEnvironment("Local");
    }

    public void CustomConfigureServicesWithMocks(IWebHostBuilder builder, ITestOutputHelper testOutputHelper)
    {
        builder.ConfigureTestServices(services =>
        {
            //override existing regirations with mocks
        })
            .UseEnvironment("Local");
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}
