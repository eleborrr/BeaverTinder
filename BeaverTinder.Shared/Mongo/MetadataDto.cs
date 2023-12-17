namespace BeaverTinder.Shared.Mongo;

public record MetadataDto(string Key, Dictionary<string, string> Data);