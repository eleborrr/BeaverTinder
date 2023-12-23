using BeaverTinder.Application.Configs;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Shared.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeaverTinder.S3.Clients.MongoClient;

public class MongoDbClient: IMongoDbClient
{
    private readonly IMongoCollection<MetadataDto> _metadataCollection;

    public MongoDbClient(IOptions<MongoDbConfig> mongoDbConfig)
    {
        var mongoClient = new MongoDB.Driver.MongoClient(
            mongoDbConfig.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbConfig.Value.DatabaseName);

        _metadataCollection = mongoDatabase.GetCollection<MetadataDto>(
            mongoDbConfig.Value.MetadataCollectionName);
    }

    public async Task<List<MetadataDto>> GetAsync() =>
        await _metadataCollection.Find(_ => true).ToListAsync();

    public async Task<MetadataDto> GetAsync(string key) =>
        await _metadataCollection.Find(b => b.Key == key).FirstOrDefaultAsync();

    public async Task CreateAsync(MetadataDto newMetadata) =>
        await _metadataCollection.InsertOneAsync(newMetadata);

    public async Task UpdateAsync(string key, MetadataDto updatedMetadata) =>
        await _metadataCollection.ReplaceOneAsync(b => b.Key == key, updatedMetadata);

    public async Task RemoveAsync(string key) =>
        await _metadataCollection.DeleteOneAsync(b => b.Key == key);
}