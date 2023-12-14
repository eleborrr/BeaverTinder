using BeaverTinder.S3.Configs;
using BeaverTinder.Shared.Files;
using Minio;
using Minio.DataModel.Args;
using MassTransit;

namespace BeaverTinder.S3.Services;

public class FileSaverConsumer: IConsumer<SaveFileMessage>
{
    private readonly IMinioClient  _minioClient;
    private readonly S3Config _s3Config;

    public FileSaverConsumer(IMinioClient minioClient, S3Config s3Config)
    {
        _minioClient = minioClient;
        _s3Config = s3Config;
    }

    public async Task Consume(ConsumeContext<SaveFileMessage> context)
    {
        try
        {
            context.Message.Deconstruct(out var file, out var metadata,out var fileIdentifier, out var bucketIdentifier);

            foreach (var data in metadata)
            {
                Console.WriteLine();
                Console.WriteLine($"{data.Key} : {data.Value}");
                Console.WriteLine();
            }
            Console.WriteLine("-------------");
            
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_s3Config.BucketName)
                .WithObject(fileIdentifier)
                .WithStreamData(new MemoryStream(file.Content))
                .WithContentType("text")
                .WithObjectSize(file.Content.Length)
                .WithHeaders(metadata);
            await _minioClient.PutObjectAsync(putObjectArgs);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}