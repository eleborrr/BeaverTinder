namespace BeaverTinder.Shared.Files;

public record GetFileMessage(string[] FileIdentifiers, string BucketIdentifier);