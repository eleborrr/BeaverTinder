using BeaverTinder.Shared.Files;
using MassTransit;
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
    
    public async Task<List<Stream>> GetFiles(IEnumerable<string> fileNames)
    {
        var res = new List<Stream>();
        foreach (var fileName in fileNames)
        {
            var _stream = new MemoryStream();   
            var getObjArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(_stream);
                });
            await _minioClient.GetObjectAsync(getObjArgs);
            res.Add(_stream);
        }

        return res;
    }
}