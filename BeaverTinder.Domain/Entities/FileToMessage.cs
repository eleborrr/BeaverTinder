namespace BeaverTinder.Domain.Entities;

public class FileToMessage
{
    public string Id { get; set; }
    public string MessageId { get; set; } = null!;
    public string FileGuidName { get; set; } = null!;
}