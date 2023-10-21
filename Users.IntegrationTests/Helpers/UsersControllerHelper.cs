using System.Net;
using System.Net.Http.Json;
using Users.Application.Features.Users.Commands.AuthenticateUser;
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

    protected async Task<(HttpStatusCode StatusCode, T? result)> Authenticate<T>(AuthenticateUserCommand command, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return await AuthenticateWithSetupClient<T>(command);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> AuthenticateWithSetupClient<T>(AuthenticateUserCommand command)
    {
        var url = UrlConstants.BaseUserURL + UrlConstants.AuthenticateUser;

        var response = await _httpClient?.PostAsJsonAsync(url, command)!;

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
