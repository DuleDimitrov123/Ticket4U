using AutoFixture.Xunit2;
using Moq;
using Reservations.Application.Features.Reservations.Queries.GetReservations;
using Reservations.Domain.Reservations;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationsQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Theory]
    [AutoMoqData]
    public async Task GetReservations([Frozen] Mock<IQueryRepository<Reservation>> repositoryMock,
        GetReservationsQueryHandler handler)
    {
        //arrange
        int numberOfReservationElements = 4;
        var reservations = new List<Reservation>();

        for (int i = 0; i < numberOfReservationElements; i++)
        {
            reservations.Add(Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3)));
        }

        repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(reservations);

        handler = new GetReservationsQueryHandler(_mapper, repositoryMock.Object);

        //act
        var results = await handler.Handle(new GetReservationsQuery(), CancellationToken.None);

        //assert
        results.Count.ShouldBe(numberOfReservationElements);

        for (int i = 0; i < numberOfReservationElements; i++)
        {
            results[i].ShowId.ShouldBe(reservations[i].ShowId);
            results[i].UserId.ShouldBe(reservations[i].UserId);
            results[i].NumberOfReservations.ShouldBe(reservations[i].NumberOfReservations.Value);
        }
    }
}
