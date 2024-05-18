using ArtBooking.Model.Auth;
using ArtBooking.Storage;

public static class UserEndPoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/user/add", (User user, AuthDbContext db) =>
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Results.Created($"/users/{user.UserId}", user);
        })
        .Produces<User>(200)
        .ProducesProblem(400)
        .WithName("userAdd")
        .WithOpenApi()
        .RequireAuthorization();
    }
}