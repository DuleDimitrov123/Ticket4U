using Reservations.IntegrationTests.Base;
using Shared.Domain.Events;
using System.Net.Http.Json;
using System.Net;
using Xunit.Abstractions;
using Reservations.IntegrationTests.Constants;
using Reservations.Api.Requests.Users;

namespace Reservations.IntegrationTests.Helpers;

public class UsersControllerHelper : BaseControllerTests
{
    private readonly ITestOutputHelper _output;

    public UsersControllerHelper(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
        _output = output;
    }

    protected Task<(HttpStatusCode StatusCode, T? result)> CreateUser<T>(CreateUserRequest createUserRequest, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return CreateShowWithSetupClient<T>(createUserRequest);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> CreateShowWithSetupClient<T>(CreateUserRequest createUserRequest)
    {
        var url = $"{UrlConstants.BaseUserURL}/{UrlConstants.CreateUserSpecificURL}";

        var response = await _httpClient?.PostAsJsonAsync(url, createUserRequest)!;

        try
        {
            var result = await response.Content.ReadFromJsonAsync<T>();

            return (response.StatusCode, result);
        }
        catch
        {
            return (response.StatusCode, default(T));
        }
    }
}
