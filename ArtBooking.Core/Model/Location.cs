namespace ArtBooking.Model;

public class Location : MetaEntity
{
    public int LocationId { get; set; }
    public int OrganizationId { get; set; }
    public required string LocationName { get; set; }
    public string? Description { get; set; }
    public Address? Address { get; set; }

    public string ExamChecksum
    {
        get
        {
            return SecurityHelpers.CalculateHMAC(this.LocationName, "ZALICZENIE20240531");
        }
    }
}