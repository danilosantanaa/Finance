using System.Security.Claims;

namespace Finance.API.Extensions;

public static class ClaimPrincipalExtension
{
    public static string GetUserName(this ClaimsPrincipal user) =>
        user.Claims.First(c => c.Type == ClaimTypes.Name)?.Value;

    public static int GetId(this ClaimsPrincipal user) =>
        int.Parse(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
}