using MediatR;

namespace Shows.Application.Features.Shows.Commands.UpdateShowLocation;

public class UpdateShowLocationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string NewLocation { get; set; }
}
