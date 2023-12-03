using BeaverTinder.Shared.Files;
using Minio;
using Minio.DataModel.Args;
using MassTransit;

namespace BeaverTinder.S3.Services;

public class FileSaverConsumer: IConsumer<SaveFileMessage>
{
    private readonly IMinioClient  _minioClient;
    private readonly string _accessKey = "F7l1mZ14Pno43XicMUHY";
    private readonly string _secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
    private readonly string _bucketName = "my-bucket";

    public FileSaverConsumer(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task Consume(ConsumeContext<SaveFileMessage> context)
    {
        Console.WriteLine(context.Message);
        try
        {
            context.Message.Deconstruct(out var file, out var fileIdentifier, out var bucketIdentifier);
            
            var bucketName = "my-bucket";
            
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileIdentifier)
                .WithStreamData(file.OpenReadStream())
                .WithContentType("text")
                .WithObjectSize(file.Length);
            await _minioClient.PutObjectAsync(putObjectArgs);

        }
        catch (Exception)
        {
            Console.WriteLine("AAAAAAA");
        }
    }
}