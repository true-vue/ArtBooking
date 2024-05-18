namespace ArtBooking.Identity;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class IdentityService
{
    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        var http = httpContextAccessor.HttpContext;
        if (http?.User.Identity?.IsAuthenticated ?? false)
        {
            int userId = int.MinValue;
            int organizationId = int.MinValue;
            // get claims for identity
            var claims = http.User.Identities.FirstOrDefault(i => i.Name == http.User.Identity.Name).Claims;

            int.TryParse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);
            int.TryParse(claims.FirstOrDefault(c => c.Type == "org")?.Value, out organizationId);
            var login = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roles = claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

            if (userId == int.MinValue) throw new InvalidOperationException("UserId cannot be set form token claim");
            this.User = new User()
            {
                UserId = userId,
                OrganizationId = organizationId == int.MinValue ? null : organizationId,
                Roles = roles,
                Login = login ?? "Unknown"
            };
            this.IsAuthenticated = true;
        }
    }

    public bool IsAuthenticated
    {
        get; private set;
    }

    public User? User
    {
        get; private set;
    } = null;
}