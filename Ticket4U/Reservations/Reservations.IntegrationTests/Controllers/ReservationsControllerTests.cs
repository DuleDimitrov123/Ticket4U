using Reservations.Api.Requests.Reservations;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Reservations.IntegrationTests.Helpers;
using Shared.Api.Middlewares;
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
    public async Task CreateReservationWithNotExistingShow()
    {
        var request = new CreateReservationRequest(InstanceConstants.User1Id, Guid.NewGuid(), 3);

        var (statusCode, result) = await CreateReservation<ErrorResponse>(request, false);

        statusCode.ShouldBe(HttpStatusCode.NotFound);

        result.ShouldNotBeNull();
        result.ExceptionMessages.ShouldContain($"{nameof(Show)} {request.ShowId} is not found");
    }

    [Fact]
    public async Task CreateReservationWithNotExistingUser()
    {
        var request = new CreateReservationRequest(Guid.NewGuid(), InstanceConstants.Show1Id, 3);

        var (statusCode, result) = await CreateReservation<ErrorResponse>(request, false);

        statusCode.ShouldBe(HttpStatusCode.NotFound);

        result.ShouldNotBeNull();
        result.ExceptionMessages.ShouldContain($"{nameof(User)} {request.UserId} is not found");
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
    public async Task GetReservationByIdNotFound()
    {
        var reservationId = Guid.NewGuid();
        var (statusCode, result) = await GetReservationById<ErrorResponse>(reservationId, false);

        statusCode.ShouldBe(HttpStatusCode.NotFound);

        result.ShouldNotBeNull();

        result.ExceptionMessages.ShouldContain($"{nameof(Reservation)} {reservationId} is not found");
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

    [Fact]
    public async Task UpdateNumberOfReservationsSuccessfully()
    {
        //update
        var request = new UpdateNumberOfReservationsRequest(5);
        var statusCode = await UpdateNumberOfReservations(InstanceConstants.ReservationId1, request, false);

        statusCode.ShouldBe(HttpStatusCode.NoContent);

        //get and check
        var (statusCodeGet, result) = await GetReservationById<ReservationResponse>(InstanceConstants.ReservationId1, false);

        statusCodeGet.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();

        result.NumberOfReservations.ShouldBe(request.NewNumberOfReservations);
    }
}
