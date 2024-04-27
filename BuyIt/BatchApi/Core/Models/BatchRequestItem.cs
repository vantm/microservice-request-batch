namespace BatchApi.Core.Models;

public record BatchRequestItem(
    BatchMethod Method,
    string Path,
    Guid RequestId,
    IDictionary<string, string>? Headers = null,
    string? Body = null);