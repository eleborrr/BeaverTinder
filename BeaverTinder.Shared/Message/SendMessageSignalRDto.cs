using BeaverTinder.Shared.Files;
using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Message;

public record SendMessageSignalRDto(string msg, IEnumerable<FormFile> files);