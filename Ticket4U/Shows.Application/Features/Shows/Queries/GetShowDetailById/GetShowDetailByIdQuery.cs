using MediatR;

namespace Shows.Application.Features.Shows.Queries.GetShowDetailById;

public class GetShowDetailByIdQuery : IRequest<ShowDetailResponse>
{
    public Guid ShowId { get; set; }
}
