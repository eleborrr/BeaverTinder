using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public record SaveFileMessage(IFormFile File, string FileName, string BucketIdentifier);