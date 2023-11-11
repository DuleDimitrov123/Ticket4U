using MediatR;

namespace Shows.Application.Features.Performers.Queries.GetPerformerDetailById;

public class GetPerformerDetailByIdQuery : IRequest<PerformerDetailResponse>
{
    public Guid Id { get; set; }
}
