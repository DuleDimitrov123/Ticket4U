using Moq;
using Shouldly;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Queries.GetReservationById;
using Reservations.Domain.Reservations;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationByIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task GetReservationById()
    {
        //arrange
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3));

        var repositoryMock = new Mock<IRepository<Reservation>>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);

        var handler = new GetReservationByIdQueryHandler(_mapper, repositoryMock.Object);

        //act
        var result = await handler.Handle(new GetReservationByIdQuery() { ReservationId = Guid.NewGuid()}, CancellationToken.None);

        //assert
        result.ShowId.ShouldBe(reservation.ShowId);
        result.UserId.ShouldBe(reservation.UserId);
        result.NumberOfReservations.ShouldBe(reservation.NumberOfReservations.Value);
    }
}
