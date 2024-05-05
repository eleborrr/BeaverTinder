using BeaverTinder.Application.Features.MongoDb.GetMetadata;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.S3.Configs;
using BeaverTinder.Shared.Files;
using BeaverTinder.Shared.Mongo;
using MediatR;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.Services.Files;

public class FileGetterService
{
    private readonly IMinioClient  _minioClient;
    private readonly S3Config _s3Config;
    private readonly IMediator _mediator;
    private readonly IMongoDbClient _mongoDbClient;

    public FileGetterService(IMinioClient minioClient, S3Config s3Config, IMediator mediator, IMongoDbClient mongoDbClient)
    {
        _minioClient = minioClient;
        _s3Config = s3Config;
        _mediator = mediator;
        _mongoDbClient = mongoDbClient;
    }
    
    public async Task<FileModelSendFront?> GetFiles(string fileName)
    {
        // var statObjectArgs = new StatObjectArgs()
        //     .WithBucket(_s3Config.MainBucketName)
        //     .WithObject(fileName);
        //
        // await _minioClient.StatObjectAsync(statObjectArgs).ConfigureAwait(false);
        
        
        var memoryStream = new MemoryStream();   
        var getObjArgs = new GetObjectArgs()
            .WithBucket(_s3Config.MainBucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });
        await _minioClient.GetObjectAsync(getObjArgs);
        memoryStream.Position = 0;

        Console.WriteLine("trying to get metadata from mongo");
        // var metadata = await _mediator.Send(new GetMetadataMongoQuery(fileName));
        try
        {
            var metadata = await _mongoDbClient.GetAsync(fileName);
            return new FileModelSendFront(memoryStream.GetBuffer(), metadata);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    private static string GetContentType(string fileName)
    {
        if (fileName.Contains(".jpg"))
        {
            return "image/jpg";
        }

        if (fileName.Contains(".jpeg"))
        {
            return "image/jpeg";
        }

        if (fileName.Contains(".png"))
        {
            return "image/png";
        }

        if (fileName.Contains(".gif"))
        {
            return "image/gif";
        }

        if (fileName.Contains(".pdf"))
        {
            return "application/pdf";
        }

        return "application/octet-stream";
    }
}