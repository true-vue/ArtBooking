namespace ArtBooking.Model;

public class Organization : MetaEntity
{
    public int OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public Address? Address { get; set; }
}