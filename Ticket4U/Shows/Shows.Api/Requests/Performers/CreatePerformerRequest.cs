namespace Shows.Api.Requests.Performers;

public record CreatePerformerRequest(string Name, Dictionary<string, string>? PerformerInfoRequests);
