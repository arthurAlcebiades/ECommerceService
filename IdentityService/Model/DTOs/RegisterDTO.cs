using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace IdentityService.Model.DTOs;

public class RegisterDTO
{
    [JsonPropertyName("id"), JsonProperty("id")]
    public long Id { get; private set; }
    
    [JsonPropertyName("username"), JsonProperty("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password_hash"), JsonProperty("password_hash")]
    public string PasswordHash { get; set; }
    
    [JsonPropertyName("email") , JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("phone"), JsonProperty("phone")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string? Cpf { get; set; }
    
    [JsonPropertyName("cnpj"), JsonProperty("cnpj")]
    public string? Cnpj { get; set; }

    [JsonPropertyName("created_at"), JsonProperty("created_at")]
    public DateTime CreeatedAt { get; set; }

    [JsonPropertyName("is_active"), JsonProperty("is_active")]
    public bool IsActive { get; set; }
}