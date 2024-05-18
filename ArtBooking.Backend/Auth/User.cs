public class User
{
    public int UserId { get; set; }
    public int? OrganizationId { get; set; }
    public required string Login { get; set; }
    public List<string> Roles { get; set; } = [];
}