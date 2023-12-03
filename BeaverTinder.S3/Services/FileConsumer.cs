using BeaverTinder.Shared.Files;
using Minio;
using Minio.DataModel.Args;
using MassTransit;

namespace BeaverTinder.S3.Services;

public class FileConsumer: IConsumer<FileMessage>
{
    private readonly IMinioClient  _minioClient;
    private readonly string _accessKey = "F7l1mZ14Pno43XicMUHY";
    private readonly string _secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
    private readonly string _bucketName = "my-bucket";

    public FileConsumer(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task Consume(ConsumeContext<FileMessage> context)
    {
        // Console.WriteLine(context.Message);
        // var file = context.Message;
        // try
        // {
            context.Message.Deconstruct(out var buffer, out var fileIdentifier, out var bucketIdentifier);
        
            var stream = new MemoryStream(buffer);
            
            var bucketName = "my-bucket";
            // var objectName = file.FileName;
            // var contentType = file.ContentType;
            //
            // file.OpenReadStream().Position = 0;
            // var _bytes = new byte[file.FormFile.Length];
            // var file_Stream = file.FormFile.OpenReadStream().Read(_bytes, 5, 5);


            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileIdentifier)
                .WithStreamData(stream)
                .WithContentType("text")
                .WithObjectSize(stream.Length);
            await _minioClient.PutObjectAsync(putObjectArgs);

        // }
        // catch (Exception)
        // {
        //     Console.WriteLine("AAAAAAA");
        // }
    }
}