using Product.Application.Contracts.Services;
using System.Security.Claims;

namespace Product.API;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue("sub");
}
