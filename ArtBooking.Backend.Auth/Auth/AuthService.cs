using System.Security.Claims;
using System.Text;
using ArtBooking.Model.Auth;
using ArtBooking.Model.DTO;
using ArtBooking.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public class AuthService
{
    private readonly IPasswordHasher<object> _passwordHasher;
    private readonly AuthDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public enum UserVerificationResult
    {
        Success,
        Failed,
    }

    public AuthService(AuthDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _passwordHasher = new PasswordHasher<object>();
        _dbContext = dbContext;
        _configuration = configuration;
        var http = httpContextAccessor.HttpContext;
        if (http?.User.Identity?.IsAuthenticated ?? false)
        {
            var userId = 0;
            // get claims for identity
            var claims = http.User.Identities.FirstOrDefault(i => i.Name == http.User.Identity.Name).Claims;

            int.TryParse(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value, out userId);

            // build in admin scenario (add condition from settings)
            if (userId == 0) this.User = GetBuildInAdminUser();
            // read user from db.
            else this.User = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);

            if (User == null) throw new InvalidOperationException($"Cannot find user account {userId}");
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

    public (UserVerificationResult, User?) VerifyUser(UserCredentials credentials)
    {

        // find user in database
        var user = _dbContext.Users.FirstOrDefault(u => u.Login == credentials.Login);
        if (user == null)
        {
            // TODO: Customize to disable with application setting: EnableBuildInAdminAccount
            var isBuildInAdminEnabled = true;
            if (isBuildInAdminEnabled && credentials.Login.ToLowerInvariant() == "admin" && credentials.Password == "Pass123#")
            {
                user = GetBuildInAdminUser();
            }

            if (user == null)
            {
                return (UserVerificationResult.Failed, user);
            }
        }

        return (UserVerificationResult.Success, user);
    }

    public (JwtSecurityToken, Func<string>) CreateAuthToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim("org", user.BelongsToOrganizationId.ToString())
        };
        // User roles can be added here
        // claims.AddRange()

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddMinutes(30);

        var tokenObject = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return (tokenObject, () => new JwtSecurityTokenHandler().WriteToken(tokenObject));
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
    }

    private User GetBuildInAdminUser()
    {
        return new User()
        {
            UserId = 0,
            Login = "Admin",
            PasswordHash = "",
            BelongsToOrganizationId = null,
            UserName = "Administator",
            UserSurname = ""
        };
    }
}