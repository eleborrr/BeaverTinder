using Contracts.Dto.Image;
using Domain.Entities;

namespace Persistence.Misc.Services.Images;

public interface IImageService
{
    public Task<IEnumerable<Image>> GetAllAsync();
    public Task<int> SaveAsyncAndGetId(ImageDto imageModel);
    public Task<Image?> GetByIdAsync(string id);
}