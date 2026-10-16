namespace api.DTOs;

public class AuthResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}
