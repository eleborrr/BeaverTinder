using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Shared.Mongo;

namespace BeaverTinder.Application.Features.MongoDb.SaveMetadata;

public record SaveMetadataMongoCommand(MetadataDto Metadata): ICommand;