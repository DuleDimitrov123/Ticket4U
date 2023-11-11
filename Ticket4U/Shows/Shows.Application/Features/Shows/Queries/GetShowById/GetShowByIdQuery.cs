using MediatR;

namespace Shows.Application.Features.Shows.Queries.GetShowById;

public class GetShowByIdQuery : IRequest<ShowResponse>
{
    public Guid ShowId { get; set; }
}
