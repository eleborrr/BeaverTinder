using System.Text.Json.Serialization;

namespace BeaverTinder.Application.Dto.Vk;

public class VkResponseDto
{
    [JsonPropertyName("response")] public VkUserDto[]? Response { get; set; }
}