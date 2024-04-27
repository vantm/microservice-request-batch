namespace BatchApi.Core.Models;

public record BatchRequest(IEnumerable<BatchRequestItem> Requests);

