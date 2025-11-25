namespace UrbanNest.Services.Identity.Application.DTOs;

public class AuthResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty; // Chuỗi JWT
    public List<string> Roles { get; set; } = new();
}