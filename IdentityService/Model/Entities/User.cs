using System.Text.Json.Serialization;

namespace IdentityService.Model.Entities;

public class User
{
    [JsonPropertyName("id")]
    public long Id { get; private set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password_hash")]
    public string PasswordHash { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("phone")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("cpf")]
    public string? Cpf { get; set; }
    
    [JsonPropertyName("cnpj")]
    public string? Cnpj { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreeatedAt { get; set; }

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}