using System.Collections.Immutable;
using System.Net;
using System.Net.Http.Headers;

namespace BatchApi.Core.Models;

public record BatchReponse(
    Guid RequestId,
    HttpStatusCode StatusCode,
    ImmutableDictionary<string, string> Headers,
    object? Content);

