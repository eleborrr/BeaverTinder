using System.Text.Json.Serialization;

namespace Contracts.Dto.Vk;

public class VkResponseDto
{
    [JsonPropertyName("response")] public VkUserDto[]? Response { get; set; }
}