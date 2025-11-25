using UrbanNest.Services.Identity.Application.DTOs;
using UrbanNest.Services.Identity.Application.DTOs.Request;

namespace UrbanNest.Services.Identity.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<bool> RegisterAsync(RegisterRequest request);
}