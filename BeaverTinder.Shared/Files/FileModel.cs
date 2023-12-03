namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(FileData File, string FileName, string BucketIdentifier);

public record FileData(byte[] Content);
