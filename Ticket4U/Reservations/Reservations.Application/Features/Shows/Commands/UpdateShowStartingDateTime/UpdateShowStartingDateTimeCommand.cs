using MediatR;

namespace Reservations.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommand  : IRequest<Unit>
{
    public Guid ExternalShowId { get; set; }

    public DateTime NewStartingDateTime { get; set; }
}