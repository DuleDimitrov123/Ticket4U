namespace Shows.Application.Features.Performers.Queries;

public record PerformerDetailResponse(string Name, IList<PerformerInfoResponse> PerformerInfos);
