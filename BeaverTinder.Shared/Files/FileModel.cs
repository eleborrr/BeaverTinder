namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(FileData File, Dictionary<string, string> Metadata, string FileName, string BucketIdentifier);

public record FileData(byte[] Content);
