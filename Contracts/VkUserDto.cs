using System.Text.Json.Serialization;

namespace Contracts;

public class VkUserDto
{
    public VkUserDto(string? dateOfBirth, int vkId, int gender, string? about, string? photoUrl)
    {
        DateOfBirth = dateOfBirth;
        VkId = vkId;
        Gender = gender;
        About = about;
        PhotoUrl = photoUrl;
    }

    [JsonPropertyName("bdate")]
    public string? DateOfBirth { get; set; }
    [JsonPropertyName("id")]
    public int VkId { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("first_name")] 
    public string FirstName { get; set; } = null!;
    [JsonPropertyName("screen_name")] 
    public string UserName { get; set; } = null!;
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("sex")]
    public int Gender { get; set; }
    [JsonPropertyName("status")]
    public string? About { get; set; }
    [JsonPropertyName("photo_max_orig")]
    public string? PhotoUrl { get; set; }
}