using AutoFixture.Xunit2;
using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;
using Reservations.Domain.Reservations;
using Reservations.UnitTests.Helpers;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationsByUserIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Theory]
    [AutoMoqData]
    public async Task GetReservationByUserId([Frozen] Mock<IReservationQueryRepository> repositoryMock,
        GetReservationsByUserIdQueryHandler handler)
    {
        //arrange
        int numberOfReservationElements = 4;
        var reservations = new List<Reservation>();

        for (int i = 0; i < numberOfReservationElements; i++)
        {
            reservations.Add(Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3)));
        }

        repositoryMock.Setup(r => r.GetReservationsByUserId(It.IsAny<Guid>())).ReturnsAsync(reservations);

        handler = new GetReservationsByUserIdQueryHandler(_mapper, repositoryMock.Object);

        //act
        var results = await handler.Handle(new GetReservationsByUserIdQuery() { UserId = Guid.NewGuid() }, CancellationToken.None);

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
