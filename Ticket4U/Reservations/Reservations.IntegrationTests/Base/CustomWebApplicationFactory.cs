using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.Infrastructure;
using Reservations.IntegrationTests.Constants;
using Shared.Application.Contracts.Persistence;
using Testcontainers.MsSql;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Base;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    //If you want to connect to db, connectionString (change port): Server=localhost,52159;Database=master;User Id=sa;Password=localdevpassword#123;
    private readonly MsSqlContainer _container =
        new MsSqlBuilder()
            .WithPassword("localdevpassword#123")
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(async services =>
        {
            services.AddTestDbContext(_container.GetConnectionString());

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ReservationsDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                context.Database.EnsureCreated();

                try
                {
                    await InitializeDbForTestsAsync(sp);
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
        var showRepository = serviceProvider.GetRequiredService<ICommandRepository<Show>>();
        var userRepository = serviceProvider.GetRequiredService<ICommandRepository<User>>();
        var reservationRepository = serviceProvider.GetRequiredService<ICommandRepository<Reservation>>();

        var show1 = Show.Create(InstanceConstants.Show1Id, "Show1", DateTime.Now.AddDays(10), 100, InstanceConstants.ExternalShow1Id);
        var user1 = User.Create(InstanceConstants.User1Id, "user1@gmail.com", "user1", InstanceConstants.ExternalUser1Id);
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
