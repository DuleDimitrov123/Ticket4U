using MediatR;

namespace Shows.Application.Features.Shows.Commands.ChangeShowStatus;

public class ChangeShowStatusCommand : IRequest<Unit>
{
    public Guid ShowId { get; set; }

    public bool IsSoldOut { get; set; }
}
