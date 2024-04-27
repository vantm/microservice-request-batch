using BatchApi.Core.Models;

namespace BatchApi.Core;

public interface IBatchExecutorFactory
{
    IBatchExecutor Create(BatchRequestItem request);
}
