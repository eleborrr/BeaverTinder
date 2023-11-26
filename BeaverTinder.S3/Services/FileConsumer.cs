using BeaverTinder.Shared.Files;
using Minio;
using Minio.DataModel.Args;
using MassTransit;

namespace BeaverTinder.S3.Services;

public class FileConsumer: IConsumer<IEnumerable<FileModel>>
{
    private readonly IMinioClient  _minioClient;
    private readonly string _accessKey = "F7l1mZ14Pno43XicMUHY";
    private readonly string _secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
    private readonly string _bucketName = "my-bucket";
    
    public async Task Consume(ConsumeContext<IEnumerable<FileModel>> context)
    {
        var files = context.Message;
        foreach (var file in files)
        {
            try
            {
                var bucketName = "my-bucket";
                var objectName = file.FormFile.FileName;
                var contentType = file.FormFile.ContentType;

                file.FormFile.OpenReadStream().Position = 0;
                // var _bytes = new byte[file.FormFile.Length];
                // var file_Stream = file.FormFile.OpenReadStream().Read(_bytes, 5, 5);
            

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(file.FormFile.OpenReadStream())
                    .WithContentType(contentType)
                    .WithObjectSize(file.FormFile.Length);
                await _minioClient.PutObjectAsync(putObjectArgs);

            }
            catch (Exception)
            {
                Console.WriteLine("AAAAAAA");
            }  
        } 
    }
}