using System.Net.Http.Headers;

namespace Shared.IntegrationTests.Authorization;

public static class HttpClientExtensions
{
    public static void SetAuthorization(this HttpClient client, AuthorizationType authorizationType)
    {
        switch (authorizationType)
        {
            case AuthorizationType.UnAuthorized:
                break;
            case AuthorizationType.BasicAuthorization:
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    TestAuthorizationHelper.CreateBasicToken());
                break;
            case AuthorizationType.AdminAuthorization:
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    TestAuthorizationHelper.CreateAdminToken());
                break;
            default:
                break;
        }
    }
}
