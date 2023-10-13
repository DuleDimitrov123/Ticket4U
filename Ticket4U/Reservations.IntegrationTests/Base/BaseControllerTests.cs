using Shared.IntegrationTests.Authorization;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Base;

public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    protected HttpClient? _httpClient;

    public BaseControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    public void SetUpClient(bool mocked, AuthorizationType authorizationType, ITestOutputHelper testOutputHelper)
    {
        if (!mocked)
        {
            _httpClient = GetNewClient(testOutputHelper);
        }
        else
        {
            _httpClient = GetNewClientWithMocks(testOutputHelper);
        }

        _httpClient.SetAuthorization(authorizationType);
    }

    private HttpClient GetNewClient(ITestOutputHelper testOutputHelper)
    {
        return _factory
            .WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServices(builder, testOutputHelper);
            })
            .CreateClient();
    }

    private HttpClient GetNewClientWithMocks(ITestOutputHelper testOutputHelper)
    {
        var client = _factory
            .WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServicesWithMocks(builder, testOutputHelper);
            }).CreateClient();

        client.Timeout = TimeSpan.FromSeconds(600);

        return client;
    }
}
