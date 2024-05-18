namespace ArtBooking.Model.Auth;

public class User
{
    public int UserId { get; set; }
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }
    public int? BelongsToOrganizationId { get; set; }
}