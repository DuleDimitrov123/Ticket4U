using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;
using Users.Application.Contracts.Identity;
using Users.Infrastructure.Identity;
using Users.IntegrationTests.Constants;
using Xunit.Abstractions;

namespace Users.IntegrationTests.Base;

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
        builder.ConfigureServices(services =>
        {
            services.AddTestDbContext(_container.GetConnectionString());

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<UsersIdentityDbContext>();
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
        var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();

        await authenticationService.RegistrateAsync(TestUsers.TestAdmin);
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
