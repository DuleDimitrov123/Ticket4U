using Reservations.Api.Requests.Reservations;
using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Shared.Domain.Events;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Helpers;

public class ShowsControllerHelper : BaseControllerTests
{
    private readonly ITestOutputHelper _output;

    public ShowsControllerHelper(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
        _output = output;
    }

    protected Task<(HttpStatusCode StatusCode, T? result)> CreateShow<T>(CreatedShowEvent createdShowEvent, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return CreateShowWithSetupClient<T>(createdShowEvent);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> CreateShowWithSetupClient<T>(CreatedShowEvent createdShowEvent)
    {
        var url = $"{UrlConstants.BaseShowURL}/{UrlConstants.CreateShowSpecificURL}";

        var response = await _httpClient?.PostAsJsonAsync(url, createdShowEvent)!;

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

    protected Task<HttpStatusCode> UpdateShowStartingDateTime(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return UpdateShowStartingDateTimeWithSetUpClient(updatedShowsStartingDateTimeEvent);
    }

    private async Task<HttpStatusCode> UpdateShowStartingDateTimeWithSetUpClient(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent)
    {
        var url = $"{UrlConstants.BaseShowURL}/{UrlConstants.UpdateShowStartingDateTimeSpecificURL}";

        var response = await _httpClient.PostAsJsonAsync(url, updatedShowsStartingDateTimeEvent);

        try
        {
            return response.StatusCode;
        }
        catch
        {
            return response.StatusCode;
        }
    }
}
