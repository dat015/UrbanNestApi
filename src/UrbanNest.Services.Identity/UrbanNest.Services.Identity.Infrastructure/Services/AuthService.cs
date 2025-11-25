using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UrbanNest.Services.Identity.Application.DTOs;
using UrbanNest.Services.Identity.Application.DTOs.Request;
using UrbanNest.Services.Identity.Application.Interfaces;
using UrbanNest.Services.Identity.Domain.Entities;

namespace UrbanNest.Services.Identity.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // 1. Tìm user theo Email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) throw new Exception("Tài khoản không tồn tại.");

        // 2. Kiểm tra Password
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new Exception("Sai mật khẩu.");

        // 3. Lấy Roles của User
        var userRoles = await _userManager.GetRolesAsync(user);

        // 4. Tạo List Claims (Những thông tin in lên tấm vé Token)
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("TenantId", user.TenantId.ToString()), // Custom Claim
            new Claim("uid", user.Id.ToString()) // Rất quan trọng để bên Apartment biết ai đang gọi
        };

        // Add thêm role vào claim
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        // 5. Ký Token (Sign)
        var token = GetToken(authClaims);

        return new AuthResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FullName = user.FullName,
            Roles = userRoles.ToList(),
            Token = new JwtSecurityTokenHandler().WriteToken(token) // Convert object thành chuỗi string
        };
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null) throw new Exception("Email đã tồn tại.");

        ApplicationUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email, // Username lấy theo email luôn cho tiện
            FullName = request.FullName
        };

        // Tạo User + Hash Password
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception("Tạo user thất bại: " + errors);
        }

        // Gán Role (Nếu chưa có role trong DB thì phải tạo trước - phần này xử lý sau ở SeedData)
        if (await _roleManager.RoleExistsAsync(request.Role))
        {
            await _userManager.AddToRoleAsync(user, request.Role);
        }

        return true;
    }

    // Hàm phụ trợ để sinh JWT Token object
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        return new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3), // Token sống 3 tiếng
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }
}