using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public record FileMessage(byte[] Bytes, string FileIdentifier, string BucketIdentifier);