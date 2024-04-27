using BatchApi.Core.Models;

namespace BatchApi.Core;

public interface IBatchProcessor
{
    void Add(BatchRequestItem definition);
    Task<BatchReply> ExecuteAsync(CancellationToken cancellationToken = default);
}
