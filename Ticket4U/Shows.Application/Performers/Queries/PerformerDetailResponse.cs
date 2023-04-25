namespace Shows.Application.Performers.Queries;

public record PerformerDetailResponse(string Name, IList<PerformerInfoResponse> PerformerInfos);
