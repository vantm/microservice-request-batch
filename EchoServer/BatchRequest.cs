namespace EchoServer;

public record BatchRequestEntry(string Path, string RequestId);
public record BatchRequest(BatchRequestEntry[] Requests);

public record BatchResponseEntry(bool Succeed, string RequestId, object Data);