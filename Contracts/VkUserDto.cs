using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts;

public class VkUserDto
{
    [JsonPropertyName("bdate")]
    public string DateOfBirth { get; set; }
    [JsonPropertyName("id")]
    public int VkId { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("screen_name")]
    public string UserName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("sex")]
    public int Gender { get; set; }
    [JsonPropertyName("status")]
    public string About { get; set; }
}