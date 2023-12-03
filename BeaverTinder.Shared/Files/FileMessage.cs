namespace BeaverTinder.Shared.Files;

public record FileMessage(FileModelSend[] Files, string FileIdentifier, string BucketIdentifier);