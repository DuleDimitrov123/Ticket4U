using MediatR;

namespace Shows.Application.Features.Performers.Queries.GetPerformers;

public class GetPerformersQuery : IRequest<IList<PerformerResponse>>
{
}
