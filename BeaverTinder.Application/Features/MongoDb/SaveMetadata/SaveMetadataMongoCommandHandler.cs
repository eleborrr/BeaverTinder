using BeaverTinder.Application.Clients.MongoClient;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Shared.Mongo;

namespace BeaverTinder.Application.Features.MongoDb.SaveMetadata;

public class SaveMetadataMongoCommandHandler: ICommandHandler<SaveMetadataMongoCommand>
{
    private readonly IMongoDbClient _mongoClient;

    public SaveMetadataMongoCommandHandler(IMongoDbClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public Task<Result> Handle(SaveMetadataMongoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mongoClient.CreateAsync(request.Metadata);
            return Task.FromResult(new Result(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result(false, e.Message));

        }
    }
}