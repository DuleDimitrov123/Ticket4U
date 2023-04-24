namespace Shows.Api.Requests.Performers;

public record CreatePerformerRequest(string Name, IList<PerformerInfoRequest>? PerformerInfoRequests);
