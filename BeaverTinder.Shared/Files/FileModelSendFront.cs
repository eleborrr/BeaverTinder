using BeaverTinder.Shared.Mongo;

namespace BeaverTinder.Shared.Files;

public record FileModelSendFront(byte[] BytesArray, MetadataDto Metadata);