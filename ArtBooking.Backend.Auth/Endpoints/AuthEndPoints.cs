using System.Security.Claims;
using System.Text;
using ArtBooking.Model.DTO;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using ArtBooking.Model.Auth;

public static class AuthEndPoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/login", (UserCredentials credentials, AuthService auth) =>
        {
            var (result, user) = auth.VerifyUser(credentials);

            if (result != AuthService.UserVerificationResult.Success)
            {
                return Results.BadRequest(new { message = "Invalid user credentials" });
            }

            var (_, WriteToken) = auth.CreateAuthToken(user);

            return Results.Ok(new { token = WriteToken() });
        });
    }
}