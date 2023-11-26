using BeaverTinder.Application.Services.S3;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController: ControllerBase
{
    private readonly IMinioClient  _minioClient;
    private readonly string _accessKey = "F7l1mZ14Pno43XicMUHY";
    private readonly string _secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
    private readonly string _bucketName = "my-bucket";
    private readonly Dictionary<string, string> _uploadKeys = new();

    public FileController(IMinioClient  minioClient)
    {
        _minioClient = minioClient;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] FileModel file)
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

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
    }
    
//     [HttpPost("/multipart")]
//     public async Task<ActionResult> PostMultipart([FromForm] FileModel file)
//     {
//         int MB = (int)Math.Pow(2, 20);
//         var config = new AmazonS3Config
//         {
//             RegionEndpoint = RegionEndpoint.USEast1,
//             ServiceURL = "http://localhost:9000",
//             ForcePathStyle = true
//         };
//         var client = new AmazonS3Client(_accessKey, _secretKey, config);
//         
//         
//         var listBucketResponse = await client.ListBucketsAsync();
//
//         foreach (var bucket in listBucketResponse.Buckets)
//         {
//             Console.Out.WriteLine("bucket '" + bucket.BucketName + "' created at " + bucket.CreationDate);
//         }
//         
//         var initResponse = await client.InitiateMultipartUploadAsync(_bucketName, file.FileName);
//         
//         _uploadKeys.Add(file.FileName, initResponse.UploadId);
//
//         UploadPartRequest uploadRequest = new UploadPartRequest
//         {
//             BucketName = _bucketName,
//             Key = file.FileName,
//             UploadId = initResponse.UploadId,
//             PartNumber = 1,
//             PartSize = 5 * MB,
//             InputStream = file.FormFile.OpenReadStream()
//         };
//         var up1Response = await client.UploadPartAsync(uploadRequest);
//
// // Complete the multipart upload
//         var compRequest = new CompleteMultipartUploadRequest
//         {
//             BucketName = _bucketName,
//             Key = file.FileName,
//             UploadId = initResponse.UploadId,
//             PartETags = new List<PartETag>
//             {
//                 new PartETag { ETag = up1Response.ETag, PartNumber = 1 },
//             }
//         };
//         // var compResponse = await client.CompleteMultipartUploadAsync(compRequest);
//
//         await client.CompleteMultipartUploadAsync(compRequest);
//         
//         return StatusCode(StatusCodes.Status201Created);
//         
//         
//     }
}