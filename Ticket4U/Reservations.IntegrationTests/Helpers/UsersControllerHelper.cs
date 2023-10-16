using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Shared.Domain.Events;
using Shared.IntegrationTests.Authorization;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Helpers;

public class UsersControllerHelper : BaseControllerTests
{
    private readonly ITestOutputHelper _output;

    public UsersControllerHelper(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
        _output = output;
    }

    protected Task<(HttpStatusCode StatusCode, T? result)> CreateUser<T>(CreatedUserEvent createUserRequest, bool mocked = true)
    {
        SetUpClient(mocked, AuthorizationType.UnAuthorized, _output);
        return CreateShowWithSetupClient<T>(createUserRequest);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> CreateShowWithSetupClient<T>(CreatedUserEvent createUserRequest)
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
