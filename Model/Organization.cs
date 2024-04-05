using System.ComponentModel.DataAnnotations;

namespace ArtBooking.Model;

public class Organization : MetaEntity
{
    [Required]
    public int OrganizationId { get; set; }

    [Required]
    [MaxLength(80)]
    public string? OrganizationName { get; set; }

    [Required]
    public Address? Address { get; set; }
}