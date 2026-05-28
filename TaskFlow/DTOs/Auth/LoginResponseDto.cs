namespace TaskFlow.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
}