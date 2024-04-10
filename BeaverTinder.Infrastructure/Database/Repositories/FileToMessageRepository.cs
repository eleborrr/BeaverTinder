using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

internal sealed class FileToMessageRepository: IFileToMessageRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public FileToMessageRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
    
    public async Task<IEnumerable<FileToMessage>> GetAllAsync(CancellationToken cancellationToken) =>
        await _applicationDbContext.Files.ToListAsync(cancellationToken);
    
    public Task<IEnumerable<FileToMessage>> GetByMessageIdAsync(string messageId) =>
        Task.FromResult(_applicationDbContext.Files.Where(x => x.MessageId == messageId).AsEnumerable());

    public async Task<string> AddAsync(FileToMessage file)
    {
        await _applicationDbContext.Files.AddAsync(file);
        await _applicationDbContext.SaveChangesAsync();
        return file.Id;
    }

    public Task<FileToMessage?> GetByNameAsync(string fileName) =>
        Task.FromResult(_applicationDbContext.Files.FirstOrDefault(f => f.FileGuidName == fileName));

    public async Task UpdateAsync(FileToMessage file)
    {
        var fileInDb = _applicationDbContext.Files.FirstOrDefault(g => g.Id == file.Id);
        if (fileInDb is null)
            return;
        _applicationDbContext.Entry(fileInDb).CurrentValues.SetValues(file);
        await _applicationDbContext.SaveChangesAsync();
    }
}