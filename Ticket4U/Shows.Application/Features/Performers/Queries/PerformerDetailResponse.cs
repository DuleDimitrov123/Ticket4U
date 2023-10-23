namespace Shows.Application.Features.Performers.Queries;

public record PerformerDetailResponse(Guid Id, string Name, IList<PerformerInfoResponse> PerformerInfos);
