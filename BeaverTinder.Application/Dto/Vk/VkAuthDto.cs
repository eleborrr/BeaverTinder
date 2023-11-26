namespace BeaverTinder.Application.Dto.Vk;

public class VkAuthDto
{
    public DateTime DateOfBirth { get; set; }
    public string VkId { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? About { get; set; }
    public string? PhotoUrl { get; set; }
}