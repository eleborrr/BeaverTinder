using BeaverTinder.Application.Dto.Image;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Infrastructure.Images;

public interface IImageService
{
    public Task<IEnumerable<Image>> GetAllAsync();
    public Task<int> SaveAsyncAndGetId(ImageDto imageModel);
    public Task<Image?> GetByIdAsync(string id);
}