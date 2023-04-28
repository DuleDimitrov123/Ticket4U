using MediatR;

namespace Shows.Application.Features.Shows.Commands.DeleteShow;

public class DeleteShowCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
