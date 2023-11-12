using AutoFixture.Xunit2;
using Moq;
using Reservations.Application.Features.Reservations.Queries.GetReservationById;
using Reservations.Domain.Reservations;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationByIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Theory]
    [AutoMoqData]
    public async Task GetReservationById([Frozen] Mock<IQueryRepository<Reservation>> repositoryMock,
        GetReservationByIdQueryHandler handler)
    {
        //arrange
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3));

        repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);

        handler = new GetReservationByIdQueryHandler(_mapper, repositoryMock.Object);

        //act
        var result = await handler.Handle(new GetReservationByIdQuery() { ReservationId = Guid.NewGuid() }, CancellationToken.None);

        //assert
        result.ShowId.ShouldBe(reservation.ShowId);
        result.UserId.ShouldBe(reservation.UserId);
        result.NumberOfReservations.ShouldBe(reservation.NumberOfReservations.Value);
    }
}
