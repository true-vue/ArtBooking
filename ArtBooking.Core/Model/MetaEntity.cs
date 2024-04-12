namespace ArtBooking.Model;

public class MetaEntity
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; } = DateTime.Now;
    // public int CreatedByUserId { get; set; }
    // public int ModifedByUSerId { get; set; }
}