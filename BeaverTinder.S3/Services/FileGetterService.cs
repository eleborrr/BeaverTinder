using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.Services;

public class FileGetterService
{
    private readonly IMinioClient  _minioClient;
    private readonly string _accessKey = "F7l1mZ14Pno43XicMUHY";
    private readonly string _secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
    private readonly string _bucketName = "my-bucket";

    public FileGetterService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    
    public async Task<byte[]> GetFiles(string fileName)
    {
        Console.WriteLine(fileName);
        Console.WriteLine("trying to read " + fileName);
        var memoryStream = new MemoryStream();   
        var getObjArgs = new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });
        await _minioClient.GetObjectAsync(getObjArgs);
        memoryStream.Position = 0;

        return memoryStream.GetBuffer();
    }
    
    private static string GetContentType(string fileName)
    {
        if (fileName.Contains(".jpg"))
        {
            return "image/jpg";
        }
        else if (fileName.Contains(".jpeg"))
        {
            return "image/jpeg";
        }
        else if (fileName.Contains(".png"))
        {
            return "image/png";
        }
        else if (fileName.Contains(".gif"))
        {
            return "image/gif";
        }
        else if (fileName.Contains(".pdf"))
        {
            return "application/pdf";
        }
        else
        {
            return "application/octet-stream";
        }
    }
}