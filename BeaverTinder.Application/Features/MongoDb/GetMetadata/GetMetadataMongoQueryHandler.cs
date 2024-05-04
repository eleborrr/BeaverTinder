// // using BeaverTinder.Application.Clients.MongoClient;
// using BeaverTinder.Application.Dto.MediatR;
// using BeaverTinder.Application.Services.Abstractions;
// using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
// using BeaverTinder.Shared.Mongo;
//
// namespace BeaverTinder.Application.Features.MongoDb.GetMetadata;
//
// public class GetMetadataMongoQueryHandler: IQueryHandler<GetMetadataMongoQuery, MetadataDto>
// {
//     private readonly IMongoDbClient _mongoDb;
//
//     public GetMetadataMongoQueryHandler(IMongoDbClient mongoDb)
//     {
//         _mongoDb = mongoDb;
//     }
//
//     public async Task<Result<MetadataDto>> Handle(GetMetadataMongoQuery request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var metadata = await _mongoDb.GetAsync(request.Key);
//             return new Result<MetadataDto>(metadata, true);
//         }
//         catch (Exception e)
//         {
//             return new Result<MetadataDto>(null, false, e.Message);
//         }
//     }
// }