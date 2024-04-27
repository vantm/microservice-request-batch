using BatchApi.Core.Models;

namespace BatchApi.Core;

public static class BatchProcessorExtensions
{
    public static void AddRange(
        this IBatchProcessor processor,
        IEnumerable<BatchRequestItem> requests)
    {
        foreach (var request in requests)
        {
            processor.Add(request);
        }
    }
}
