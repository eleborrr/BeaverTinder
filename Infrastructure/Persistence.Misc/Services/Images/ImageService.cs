using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Misc.Services.Images;

public class ImageService: IImageService
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ApplicationDbContext _dbContext;
    
    public ImageService(IWebHostEnvironment hostEnvironment, ApplicationDbContext dbContext)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<int> SaveAsyncAndGetId(ImageDto imageModel)
    {
        var wwwRootPath = _hostEnvironment.WebRootPath;
        const string folder = "/Image/";
        
        var path = Path.Combine(wwwRootPath + folder + imageModel.ImageName); //wwwRootPath + "/Image/ + imageModel.ImageName.Split(".")[0]

        await using (var fileStream = new FileStream(path,FileMode.Create))
        {
            await imageModel.ImageFile.CopyToAsync(fileStream);
        }
        
        _dbContext.Images.Add(new Image
        {
            ImagePath = folder + imageModel.ImageName,
            ImageName = imageModel.ImageName
        });
        await _dbContext.SaveChangesAsync();
        
        return _dbContext.Images
            .FirstOrDefaultAsync(i => i.ImageName == imageModel.ImageName).Id;
    }

    public async Task<Image> GetByIdAsync(string id)
    {
        return await _dbContext.Images.Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _dbContext.Images.ToListAsync();
    }
}