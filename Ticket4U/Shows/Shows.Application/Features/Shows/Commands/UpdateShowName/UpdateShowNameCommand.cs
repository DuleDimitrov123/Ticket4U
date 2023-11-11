using MediatR;

namespace Shows.Application.Features.Shows.Commands.UpdateShowName;

public class UpdateShowNameCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string NewName { get; set; }
}
