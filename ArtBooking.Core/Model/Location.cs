namespace ArtBooking.Model;

public class Location : MetaEntity
{
    public int LocationId { get; set; }
    public int OrganizationId { get; set; }
    public string? LocationName { get; set; }
    public string? Description { get; set; }
    public Address? Address { get; set; }
}