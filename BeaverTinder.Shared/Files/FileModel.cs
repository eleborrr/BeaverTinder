using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(IFormFile Bytes, string FileName, string BucketIdentifier);