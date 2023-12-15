using BeaverTinder.Application.Features.Redis.SaveCache;
using BeaverTinder.S3.Configs;
using BeaverTinder.Shared.Files;
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
            context.Message.Deconstruct(out var file, out var metadata,out var fileIdentifier, out var bucketIdentifier);

            // save metadata in redis via mediator.
            await _mediator.Send(new SaveCacheCommand(fileIdentifier, Convert.ToBase64String(file.Content)));
            // If success ->
            // set 1 for file counter in redis            
            
            // save file
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketIdentifier)
                .WithObject(fileIdentifier)
                .WithStreamData(new MemoryStream(file.Content))
                .WithContentType("text")
                .WithObjectSize(file.Content.Length);
            await _minioClient.PutObjectAsync(putObjectArgs); // if success ->
            // If success ->
            
            // set 2 for file counter in redis
            // move file in another bucket, metadata in mongo 

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}