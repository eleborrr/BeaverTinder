﻿using System.Text.Json.Serialization;

namespace BeaverTinder.Application.Dto.Vk;
public class VkAccessTokenDto
{
    [JsonPropertyName("access_token")] 
    public string Token { get; set; } = null!;
    [JsonPropertyName("expires_in")]
    public int Expires { get; set; }
    [JsonPropertyName("user_id")]
    public int Id { get; set; }

    [JsonPropertyName("email")] 
    public string Email { get; set; } = null!;
}