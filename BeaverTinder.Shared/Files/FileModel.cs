namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(FileData File, Dictionary<string, string> Metadata, string FileName, string MainBucketIdentifier, string TemporaryBucketIdentifier);

public record FileData(byte[] Content);
