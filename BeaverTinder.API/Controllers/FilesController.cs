using BeaverTinder.Application.Dto.ResponsesAbstraction;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Files;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class FilesController: Controller
{
    private readonly IBus _bus;
    
    public FilesController(UserManager<User> userManager, IMediator mediator, IBus bus)
    {
        _bus = bus;
    }
    
    public class FileUploadModel
    {
        public IFormFileCollection Files { get; set; } = default!;
        public Dictionary<string, string> Metadata { get; set; } = default!;
    }

    [HttpPost("/uploadFile")]
    public async Task<JsonResult> UploadFile(
        [FromForm] FileUploadModel model)
    {   
        try
        {
            var result = new List<string>();
            // pass in Service all files; Send them with IBus to S3 service; With redis save cache;  ??
            foreach (var file in model.Files)
            {
                var fileDto = new SaveFileMessage
                    (new FileData(await ConvertIFormFileToByteArray(file)),
                        model.Metadata,
                        Guid.NewGuid().ToString(),
                        "main-bucket",
                        "temporary-bucket");
               
                result.Add(fileDto.FileName);
            
                if (file.Length > 0)
                    await _bus.Publish(fileDto);
                
            }
            
            return Json(result);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
    
    private static async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

}