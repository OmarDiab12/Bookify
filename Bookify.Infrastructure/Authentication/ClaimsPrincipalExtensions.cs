using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Bookify.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userID = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(userID, out Guid parsedUserId) ? parsedUserId : throw new ApplicationException("User ID not found.");
    }

    public static string GetIdentityId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ApplicationException("Identity ID not found.");
    }
}
