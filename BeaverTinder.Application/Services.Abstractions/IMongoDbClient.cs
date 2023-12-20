using BeaverTinder.Shared.Mongo;

namespace BeaverTinder.Application.Services.Abstractions;

public interface IMongoDbClient
{
    public Task<List<MetadataDto>> GetAsync();

    public Task<MetadataDto> GetAsync(string key);

    public Task CreateAsync(MetadataDto newMetadata);

    public Task UpdateAsync(string key, MetadataDto updatedMetadata);

    public Task RemoveAsync(string key);
}