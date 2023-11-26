using BeaverTinder.Shared.Files;

namespace BeaverTinder.Shared.Message;

public record SendMessageSignalRDto(string msg, IEnumerable<FileModel> files);