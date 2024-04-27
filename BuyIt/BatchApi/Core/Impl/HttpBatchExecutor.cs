using BatchApi.Core.Models;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;
using System.Net.Http.Headers;

namespace BatchApi.Core.Impl;

public class HttpBatchExecutor(
    IOptions<BatchOptions> options,
    IHttpClientFactory httpClientFactory) : IBatchExecutor
{
    public async ValueTask<BatchReponse> ExecuteAsync(
        BatchRequestItem request, CancellationToken cancellationToken = default)
    {
        var (method, path, requestId, headers, content) = request;
        var (servicePath, apiPath) = SplitPath(path);

        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Parse(method.ToString()), apiPath);

        if (headers is not null &&
            options.Value is { AllowedRequestHeaders: var allowedRequestHeader })
        {
            foreach (var (key, value) in headers)
            {
                if (allowedRequestHeader.Contains(key, StringComparer.OrdinalIgnoreCase))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation(key, value);
                }
            }
        }

        if (method != BatchMethod.GET && content is not null)
        {
            httpRequestMessage.Content = new StringContent(content);
        }

        using var client = httpClientFactory.CreateClient(servicePath);

        var response = await client.SendAsync(httpRequestMessage, cancellationToken);

        return new BatchReponse(
            requestId,
            response.StatusCode,
            MapResponseHeaders(response.Headers),
            await response.Content.ReadFromJsonAsync<object>(cancellationToken));
    }

    private static ImmutableDictionary<string, string> MapResponseHeaders(HttpResponseHeaders headers)
    {
        var responseHeaders = new Dictionary<string, string>();

        foreach (var header in headers)
        {
            var headerValue = string.Join(',', header.Value);
            responseHeaders.Add(header.Key, headerValue);
        }

        return responseHeaders.ToImmutableDictionary();
    }

    private static (string, string) SplitPath(string path)
    {
        var index = path.IndexOf('/', startIndex: 1);
        if (index >= 0)
        {
            return (path[..index], path[index..]);
        }
        return (string.Empty, path);
    }
}
