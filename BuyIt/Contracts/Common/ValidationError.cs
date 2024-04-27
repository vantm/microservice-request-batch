namespace Contracts.Common;

public interface ValidationError
{
    IDictionary<string, string[]> Errors { get; }
}

