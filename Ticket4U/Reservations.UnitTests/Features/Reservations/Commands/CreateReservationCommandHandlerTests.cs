using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Commands.CreateReservation;
using Reservations.Domain.Reservations;
using Shouldly;
using System.Reflection;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class CreateReservationCommandHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task CreateNewReservation()
    {
        //arrange
        var reservations = new List<Reservation>();

        var mockRepository = new Mock<IRepository<Reservation>>();
        mockRepository.Setup(repo => repo.Add(It.IsAny<Reservation>()))
            .ReturnsAsync(
                (Reservation res) =>
                {
                    //set Id by reflection...repo is mocked and won't create one
                    var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
                    reservationPropertyInfo!.SetValue(res, Guid.NewGuid());

                    reservations.Add(res);
                    return res;
                });

        var command = new CreateReservationCommand()
        {
            ShowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            NumberOfReservations = 3
        };

        var handler = new CreateReservationCommandHandler(mockRepository.Object);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.ShouldNotBe(Guid.Empty);
    }
}
