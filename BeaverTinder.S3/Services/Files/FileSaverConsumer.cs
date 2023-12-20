using BeaverTinder.Application.Features.MongoDb.SaveMetadata;
using BeaverTinder.Application.Features.Redis.DeleteCache;
using BeaverTinder.Application.Features.Redis.SaveCache;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.S3.Clients.MongoClient;
using BeaverTinder.Shared.Files;
using BeaverTinder.Shared.Mongo;
using MassTransit;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using StackExchange.Redis;

namespace BeaverTinder.S3.Services.Files;

public class FileSaverConsumer: IConsumer<SaveFileMessage>
{
    private readonly IMinioClient  _minioClient;
    private readonly IMediator _mediator;
    private readonly IMongoDbClient _mongoDbClient;

    public FileSaverConsumer(IMinioClient minioClient, IMediator mediator, IMongoDbClient mongoDbClient)
    {
        _minioClient = minioClient;
        _mediator = mediator;
        _mongoDbClient = mongoDbClient;
    }

    public async Task Consume(ConsumeContext<SaveFileMessage> context)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine("message deconstructing...");
            context.Message.Deconstruct(out var file, out var metadata,out var fileIdentifier, out var mainBucketIdentifier, out var temporaryBucketIdentifier);

            Console.WriteLine("saving metadata in redis...");
            // save metadata in redis via mediator.
            await _mediator.Send(new SaveMetadataRedisCommand(fileIdentifier, metadata));
            
            // set 1 for file counter in redis            
            IncrementFileCounterRedis();
            
            // save file
            
            Console.WriteLine("saving file...");
            await SaveFileInBucket(temporaryBucketIdentifier, fileIdentifier, file);
            
            // set 2 for file counter in redis
            IncrementFileCounterRedis();

            // move file in another bucket, metadata in mongo
            
            Console.WriteLine("saving file in main bucket...");
            await SaveFileInBucket(mainBucketIdentifier, fileIdentifier, file);
            Console.WriteLine("removing file from temp...");
            await MoveFromBucketToBucket(temporaryBucketIdentifier, fileIdentifier, mainBucketIdentifier);
            Console.WriteLine("saving metadata in mongo...");
            MoveMetadataInMongo(fileIdentifier, metadata);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task SaveFileInBucket(string bucketIdentifier, string fileIdentifier, FileData file)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketIdentifier)
            .WithObject(fileIdentifier)
            .WithStreamData(new MemoryStream(file.Content))
            .WithContentType("text")
            .WithObjectSize(file.Content.Length);
        await _minioClient.PutObjectAsync(putObjectArgs);
    }

    private async Task MoveFromBucketToBucket(string sourceBucket, string fileIdentifier, string destinationBucket)
    {
        // Copy the file to another bucket
        // var cpSrcArgs = new CopySourceObjectArgs()
        //     .WithBucket(sourceBucket)
        //     .WithObject(fileIdentifier);
        //
        // var copyObjectArgs = new CopyObjectArgs()
        //     .WithBucket(destinationBucket)
        //     .WithObject(fileIdentifier)
        //     .WithContentType("text")
        //     .WithCopyObjectSource(cpSrcArgs);
        // await _minioClient.CopyObjectAsync(copyObjectArgs);

        // Delete the original file
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(sourceBucket)
            .WithObject(fileIdentifier);
        
        await _minioClient.RemoveObjectAsync(removeObjectArgs);
    }

    private void MoveMetadataInMongo(string fileIdentifier, Dictionary<string, string> metadata)
    {
        _mongoDbClient.CreateAsync(new MetadataDto()
        {
            Key = fileIdentifier,
            Data = metadata
        });
        // _mediator.Send(new SaveMetadataMongoCommand(new MetadataDto(fileIdentifier, metadata)));
        _mediator.Send(new DeleteCacheFromRedisCommand(fileIdentifier));
    }

    private void IncrementFileCounterRedis()
    {
        
    }
}