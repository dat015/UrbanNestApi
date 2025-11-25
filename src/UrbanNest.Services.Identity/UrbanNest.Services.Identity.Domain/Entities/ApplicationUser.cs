using Microsoft.AspNetCore.Identity;

namespace UrbanNest.Services.Identity.Domain.Entities;

// Ta kế thừa IdentityUser<Guid> để ID là dạng Guid cho chuẩn
public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;
    public Guid TenantId { get; set; } // Để biết user thuộc ban quản lý nào

    // Refresh Token dùng để cấp lại token mới khi hết hạn (quan trọng cho App Mobile)
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}