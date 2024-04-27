using BatchApi.Core.Models;

namespace BatchApi.Core;

public interface IBatchExecutor
{
    ValueTask<BatchReponse> ExecuteAsync(
        BatchRequestItem request, CancellationToken cancellationToken = default);
}
