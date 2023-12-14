using BeaverTinder.S3.Configs;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.Services;

public class FileGetterService
{
    private readonly IMinioClient  _minioClient;
    private readonly S3Config _s3Config;

    public FileGetterService(IMinioClient minioClient, S3Config s3Config)
    {
        _minioClient = minioClient;
        _s3Config = s3Config;
    }
    
    public async Task<byte[]> GetFiles(string fileName)
    {
        var memoryStream = new MemoryStream();   
        var getObjArgs = new GetObjectArgs()
            .WithBucket(_s3Config.BucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });
        await _minioClient.GetObjectAsync(getObjArgs);
        memoryStream.Position = 0;
        
        var statObjectArgs = new StatObjectArgs()
            .WithBucket(_s3Config.BucketName)
            .WithObject(fileName);

        var objectStat = await _minioClient.StatObjectAsync(statObjectArgs).ConfigureAwait(false);
        return memoryStream.GetBuffer();
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