using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Shared.Mongo;

namespace BeaverTinder.Application.Features.MongoDb.GetMetadata;

public record GetMetadataMongoQuery(string Key): IQuery<MetadataDto>;