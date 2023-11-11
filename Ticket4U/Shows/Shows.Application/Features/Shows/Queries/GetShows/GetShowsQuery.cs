using MediatR;

namespace Shows.Application.Features.Shows.Queries.GetShows;

public class GetShowsQuery : IRequest<IList<ShowResponse>>
{
}
