using BeaverTinder.Application.Features.MongoDb.SaveMetadata;
using BeaverTinder.Application.Features.Redis.SaveCache;
using BeaverTinder.S3.Configs;
using BeaverTinder.Shared.Files;
using BeaverTinder.Shared.Mongo;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.Services.Files;

public class FileSaverConsumer: IConsumer<SaveFileMessage>
{
    private readonly IMinioClient  _minioClient;
    private readonly IMediator _mediator;

    public FileSaverConsumer(IMinioClient minioClient, IMediator mediator)
    {
        _minioClient = minioClient;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SaveFileMessage> context)
    {
        try
        {
            context.Message.Deconstruct(out var file, out var metadata,out var fileIdentifier, out var mainBucketIdentifier, out var temporaryBucketIdentifier);

            // save metadata in redis via mediator.
            await _mediator.Send(new SaveMetadataRedisCommand(fileIdentifier, metadata));
            
            // set 1 for file counter in redis            
            IncrementFileCounterRedis();
            
            // save file
            await SaveFileInBucket(temporaryBucketIdentifier, fileIdentifier, file);
            
            // set 2 for file counter in redis
            IncrementFileCounterRedis();

            // move file in another bucket, metadata in mongo 
            await MoveFromBucketToBucket(temporaryBucketIdentifier, fileIdentifier, mainBucketIdentifier);
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
        var cpSrcArgs = new CopySourceObjectArgs()
            .WithBucket(sourceBucket)
            .WithObject(fileIdentifier);

        var copyObjectArgs = new CopyObjectArgs()
            .WithBucket(destinationBucket)
            .WithObject(fileIdentifier)
            .WithCopyObjectSource(cpSrcArgs);
        await _minioClient.CopyObjectAsync(copyObjectArgs);

        // Delete the original file
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(sourceBucket)
            .WithObject(fileIdentifier);
        
        await _minioClient.RemoveObjectAsync(removeObjectArgs);
    }

    private void MoveMetadataInMongo(string fileIdentifier, Dictionary<string, string> metadata)
    {
        _mediator.Send(new SaveMetadataMongoCommand(new MetadataDto(fileIdentifier, metadata)));
    }

    private void IncrementFileCounterRedis()
    {
        
    }
}