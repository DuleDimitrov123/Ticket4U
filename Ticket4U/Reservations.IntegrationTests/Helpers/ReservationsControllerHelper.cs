using Microsoft.VisualStudio.TestPlatform.TestHost;
using Reservations.Api.Requests.Reservations;
using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Helpers;

public class ReservationsControllerHelper : BaseControllerTests
{
    private readonly ITestOutputHelper _output;

    public ReservationsControllerHelper(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
        _output = output;
    }

    protected Task<(HttpStatusCode StatusCode, T? result)> CreateReservation<T>(CreateReservationRequest request, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return CreateReservationWithSetUpClient<T>(request);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> CreateReservationWithSetUpClient<T>(CreateReservationRequest request)
    {
        var url = UrlConstants.BaseReservationURL;

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

    protected Task<(HttpStatusCode StatusCode, T? result)> GetReservationById<T>(Guid reservationId, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return GetReservationByIdWithSetUpClient<T>(reservationId);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> GetReservationByIdWithSetUpClient<T>(Guid reservationId)
    {
        var url = $"{UrlConstants.BaseReservationURL}/{reservationId}";

        var response = await _httpClient.GetAsync(url);

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

    protected Task<(HttpStatusCode StatusCode, T? result)> GetReservationByUserId<T>(Guid reservationId, bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return GetReservationByUserIdWithSetUpClient<T>(reservationId);
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> GetReservationByUserIdWithSetUpClient<T>(Guid userId)
    {
        var url = $"{UrlConstants.BaseReservationURL}{UrlConstants.GetReservationByUserIdSpecificURL}/{userId}";

        var response = await _httpClient.GetAsync(url);

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

    protected Task<(HttpStatusCode StatusCode, T? result)> GetReservations<T>(bool mocked = true)
    {
        SetUpClient(mocked, _output);
        return GetReservationsWithSetUpClient<T>();
    }

    private async Task<(HttpStatusCode StatusCode, T? result)> GetReservationsWithSetUpClient<T>()
    {
        var url = $"{UrlConstants.BaseReservationURL}";

        var response = await _httpClient.GetAsync(url);

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
