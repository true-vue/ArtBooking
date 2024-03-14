namespace ArtBooking.Storage;

using System.Reflection;
using ArtBooking.Model;

public class ArtBookingDataMockup
{
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

    // private List<Location> locations = new List<Location>() {
    //     new Location() {
    //         LocationId = 1,
    //         OrganizationId = 1,
    //         LocationName = "Sala główna",
    //         Adress = new Address() {
    //             Street = "Karmelicka",
    //             AddressNumber = "6",
    //             Town = "Kraków",
    //             PostalCode = "31-128"
    //         }
    //     },
    //     new Location() {
    //         LocationId = 2,
    //         OrganizationId = 2,
    //         LocationName = "Sala główna",
    //         Adress = new Address() {
    //             Street = "Karmelicka",
    //             AddressNumber = "6",
    //             Town = "Kraków",
    //             PostalCode = "31-128"
    //         }
    //     }
    // };

    // private List<Event> events = new List<Event>() {
    //     new() {
    //         EventName = "Poskromienie złośnicy",
    //         EventDescription = "Spektakl oparty o sztukę o tym samym tytule...",
    //         OrganizationId = 1
    //     },
    //     new() {
    //         EventName = "Diuna",
    //         EventDescription = "Doskonałe kino science-fiction...",
    //         OrganizationId = 2
    //     }
    // };

    public MockupList<Organization> Organizations { get { return organizations; } }
    // public List<Event> Events { get { return events; } }
    // public List<Location> Locations { get { return locations; } }
}

public class MockupList<T> : List<T> where T : class
{

    private int _identity = 0;
    private readonly PropertyInfo _idProperty;

    public MockupList(string idPropertyName)
    {
        var keyProp = typeof(T).GetProperty(idPropertyName);
        if (keyProp == null) throw new InvalidOperationException($"Identity property '{idPropertyName}'");
        _idProperty = keyProp;
    }

    /// <summary>
    /// Adds new item to mockup list.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public new T Add(T item)
    {
        _identity++;
        // set id
        _idProperty.SetValue(item, _identity);
        base.Add(item);
        return item;
    }

    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public new IEnumerable<T> AddRange(IEnumerable<T> items)
    {
        items.ToList().ForEach(i =>
        {
            _identity++;
            _idProperty.SetValue(i, _identity);
        });
        base.AddRange(items);

        return items;
    }
}