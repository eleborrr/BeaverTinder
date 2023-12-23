namespace BeaverTinder.Shared.Files;

public record FileMessage(FileModelSendFront[] Files, string FileIdentifier, string BucketIdentifier);