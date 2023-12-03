using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(byte[] Bytes, string FileIdentifier, string BucketIdentifier);