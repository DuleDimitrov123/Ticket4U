using MediatR;

namespace Shows.Application.Performers.Queries.GetPerformerDetailById;

public class GetPerformerDetailByIdQuery : IRequest<PerformerDetailResponse>
{
    public Guid Id { get; set; }
}
