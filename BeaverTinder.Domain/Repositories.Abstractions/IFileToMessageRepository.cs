using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IFileToMessageRepository
{
    public Task<IEnumerable<FileToMessage>> GetAllAsync(CancellationToken cancellationToken);
    public Task<string> AddAsync(FileToMessage geolocation);
    public Task<IEnumerable<FileToMessage>>? GetByMessageIdAsync(string messageId);
    public Task<FileToMessage?> GetByNameAsync(string fileName);
    public Task UpdateAsync(FileToMessage file);
}