using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace BeaverTinder.Application.Dto.Vk;

public class VkUserDto
{
    [JsonPropertyName("bdate")]
    public string? DateOfBirth { get; [UsedImplicitly] init; }
    [JsonPropertyName("id")]
    public int VkId { get; [UsedImplicitly] init; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("first_name")] 
    public string FirstName { get; set; } = null!;
    [JsonPropertyName("screen_name")] 
    public string UserName { get; set; } = null!;
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("sex")]
    public int Gender { get; [UsedImplicitly] init; }
    [JsonPropertyName("status")]
    public string? About { get; [UsedImplicitly] init; }
    [JsonPropertyName("photo_max_orig")]
    public string? PhotoUrl { get; [UsedImplicitly] init; }
}