using System.ComponentModel.DataAnnotations;

namespace ArtBooking.Model;

public class Address
{
    [Required]
    [MaxLength(100)]
    public string? Street { get; set; }

    /// <summary>
    /// Refers to building or building and apartment number on the street
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string? AddressNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Town { get; set; }

    [Required]
    [MaxLength(100)]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = "Polska";
}
