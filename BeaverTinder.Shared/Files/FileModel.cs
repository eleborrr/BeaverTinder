using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(IFormFile Bytes, string FileIdentifier, string BucketIdentifier);