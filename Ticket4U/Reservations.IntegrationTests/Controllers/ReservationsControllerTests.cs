using Reservations.Api.Requests.Reservations;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Common.Constants;
using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Reservations.IntegrationTests.Helpers;
using Shouldly;
using System.Net;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Controllers;

public class ReservationsControllerTests : ReservationsControllerHelper
{
    public ReservationsControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
    }

    [Fact]
    public async Task CreateReservationSuccessfully()
    {
        var request = new CreateReservationRequest(InstanceConstants.User1Id, InstanceConstants.Show1Id, 3);

        var (statusCode, result) = await CreateReservation<Guid>(request, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetReservationByIdSuccessfully()
    {
        var (statusCode, result) = await GetReservationById<ReservationResponse>(InstanceConstants.ReservationId1, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();

        result.UserId.ShouldBe(InstanceConstants.User1Id);
        result.ShowId.ShouldBe(InstanceConstants.Show1Id);
        result.NumberOfReservations.ShouldNotBe(0);
    }

    [Fact]
    public async Task GetReservationByUserIdSuccessfully()
    {
        var (statusCode, result) = await GetReservationByUserId<IList<ReservationResponse>>(InstanceConstants.User1Id, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();

        result.Count.ShouldNotBe(0);

        var r = result[0];

        r.UserId.ShouldBe(InstanceConstants.User1Id);
        r.ShowId.ShouldBe(InstanceConstants.Show1Id);
        r.NumberOfReservations.ShouldNotBe(0);
    }

    [Fact]
    public async Task GetReservationsSuccessfully()
    {
        var (statusCode, result) = await GetReservations<IList<ReservationResponse>>(false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();

        result.Count.ShouldNotBe(0);

        var r = result[0];

        r.UserId.ShouldBe(InstanceConstants.User1Id);
        r.ShowId.ShouldBe(InstanceConstants.Show1Id);
        r.NumberOfReservations.ShouldNotBe(0);
    }
}
