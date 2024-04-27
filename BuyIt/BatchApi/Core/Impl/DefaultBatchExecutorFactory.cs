using BatchApi.Core.Models;
using Microsoft.Extensions.Options;

namespace BatchApi.Core.Impl;

public class DefaultBatchExecutorFactory(
    IOptions<BatchOptions> options,
    IHttpClientFactory httpClientFactory) : IBatchExecutorFactory
{
    public IBatchExecutor Create(BatchRequestItem request)
    {
        var query = from c in options.Value.Endpoints
                    where request.Path.StartsWith(c.ServicePath, StringComparison.OrdinalIgnoreCase)
                    select c;

        if (query.Any())
        {
            return new HttpBatchExecutor(options, httpClientFactory);
        }

        return new UnsupportedBatchExecutor(request.RequestId);
    }
}
