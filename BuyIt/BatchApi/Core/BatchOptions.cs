using System.ComponentModel.DataAnnotations;

namespace BatchApi.Core;

public class BatchOptions
{
    public const string Name = "Batch";

    [Required]
    public int Timeout { get; set; } = 15;

    public IEnumerable<string> AllowedRequestHeaders { get; set; } = [];

    public IEnumerable<string> AllowedResponseHeaders { get; set; } = [];

    [Required]
    public BatchServiceEndpoint[] Endpoints { get; set; } = [];
}

public class BatchServiceEndpoint
{
    [Required]
    public string ServicePath { get; set; } = "";

    [Required]
    [Url]
    public string Url { get; set; } = "";
}
