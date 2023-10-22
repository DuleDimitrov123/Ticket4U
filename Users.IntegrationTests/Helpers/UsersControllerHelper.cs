using System.Net;
using System.Net.Http.Json;
using Users.Api.Controllers.Requests;
using Users.IntegrationTests.Base;
using Users.IntegrationTests.Constants;
using Xunit.Abstractions;

namespace Users.IntegrationTests.Helpers;

public class UsersControllerHelper : BaseControllerTests
{
    private readonly ITestOutputHelper _output;

    public UsersControllerHelper(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
    }

    protected async Task<(HttpStatusCode StatusCode, T? result)> Authenticate<T>(AuthenticateUserRequest request, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return await AuthenticateWithSetupClient<T>(request);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> AuthenticateWithSetupClient<T>(AuthenticateUserRequest request)
    {
        var url = UrlConstants.BaseUserURL + UrlConstants.AuthenticateUser;

        var response = await _httpClient?.PostAsJsonAsync(url, request)!;

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

    protected async Task<(HttpStatusCode StatusCode, T? result)> Register<T>(RegisterUserRequest request, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return await RegisterWithSetupClient<T>(request);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> RegisterWithSetupClient<T>(RegisterUserRequest request)
    {
        var url = UrlConstants.BaseUserURL + UrlConstants.RegisterUser;

        var response = await _httpClient?.PostAsJsonAsync(url, request)!;

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
