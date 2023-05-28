using System.Text.Json.Serialization;

namespace Contracts;

public class VkResponseDto
{
    [JsonPropertyName("response")] public VkUserDto[] Response { get; set; }
}