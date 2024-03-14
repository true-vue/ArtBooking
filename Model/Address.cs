namespace ArtBooking.Model;

public class Address
{
    public string? Street { get; set; }

    /// <summary>
    /// Refers to building or building and apartment number on the street
    /// </summary>
    public string? AddressNumber { get; set; }
    public string? Town { get; set; }

    public string? PostalCode { get; set; }

    public string Country { get; set; } = "Polska";
}
