namespace ArtBooking.Storage;

using System.Reflection;
using ArtBooking.Model;

public class ArtBookingDataMockup
{
    public ArtBookingDataMockup()
    {
        var firstOrg = organizations[0];
        this.locations.Add(new Location()
        {
            OrganizationId = firstOrg.OrganizationId,
            LocationId = 0,
            LocationName = "Duża scena",
            Description = "Duża scena mnieszcząca się w siedzibie głównej teatru.",
            Address = firstOrg.Address
        });
    }

    private MockupList<Organization> organizations = new MockupList<Organization>("OrganizationId") {
        new Organization() {
            OrganizationName = "Teatr Bagatela",
            // Karmelicka 6, 31-128 Kraków
            Address = new Address() {
                Street = "Karmelicka",
                AddressNumber = "6",
                Town = "Kraków",
                PostalCode = "31-128"
            },
        },
        new Organization() {
            OrganizationName = "Kino Kijów",
            // Aleja Zygmunta Krasińskiego 34, 30-101 Kraków
            Address = new Address() {
                Street = "Aleja Zygmunta Krasińskiego",
                AddressNumber = "34",
                Town = "Kraków",
                PostalCode = "30-101"
            }
        }
    };

    private MockupList<Location> locations = new MockupList<Location>("LocationId");

    public MockupList<Organization> Organizations { get { return organizations; } }
    // public List<Event> Events { get { return events; } }
    public MockupList<Location> Locations { get { return locations; } }
}