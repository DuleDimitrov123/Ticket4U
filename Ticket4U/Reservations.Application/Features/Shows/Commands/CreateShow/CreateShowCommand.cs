using MediatR;

namespace Reservations.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public DateTime StartingDateTime { get; set; }

    public int NumberOfPlaces { get; set; }

    public Guid ExternalId { get; set; }
}
