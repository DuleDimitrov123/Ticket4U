namespace Shows.Api.Requests.Performers;

public record CreatePerformerRequest(string Name, IList<CreatePerformerInfoRequest>? CreatePerformerInfoRequests);

public record CreatePerformerInfoRequest(string Name, string Value);
