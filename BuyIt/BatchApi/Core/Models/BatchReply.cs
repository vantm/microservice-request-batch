namespace BatchApi.Core.Models;

public record BatchReply(IEnumerable<BatchReponse> Responses)
{
    public static readonly BatchReply Empty = new([]);
}

