using MediatR;

namespace Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommand : IRequest<Unit>
{
    public Guid ShowId { get; set; }

    public DateTime NewStartingDateTime { get; set; }
}
