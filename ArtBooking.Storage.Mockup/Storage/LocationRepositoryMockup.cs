namespace ArtBooking.Storage.Mockup;
using ArtBooking.Model;

public class LocationRepositoryMockup : ILocationRepository
{
    private readonly ArtBookingDataMockup _context;

    public LocationRepositoryMockup(ArtBookingDataMockup context)
    {
        _context = context;
    }

    public async Task<IQueryable<Location>> GetMultipleAsync()
    {
        return await Task.Run(() => _context.Locations.AsQueryable());
    }

    public async Task<Location?> GetAsync(int id)
    {
        return await Task.Run(() => _context.Locations.Find(o => o.LocationId == id));
    }

    public async Task<Location> AddAsync(Location location)
    {
        return await Task.Run(() => _context.Locations.Add(location));
    }

    public async Task<Location> UpdateAsync(Location location)
    {
        var id = location.LocationId;
        return await Task.Run(() =>
        {
            var storedIdx = _context.Locations.FindIndex(o => o.LocationId == id);
            if (storedIdx == -1)
            {
                throw new InvalidOperationException($"Location with id:{id} cannot be found in storage.");
            }
            _context.Locations[storedIdx] = location;

            return location;
        });
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Run(() =>
        {
            var storedOrganizationIdx = _context.Organizations.FindIndex(o => o.OrganizationId == id);
            if (storedOrganizationIdx == -1)
            {
                throw new InvalidOperationException($"Organization with id:{id} cannot be found in storage.");
            }
            _context.Organizations.RemoveAt(storedOrganizationIdx);
        });
    }

    public async Task<IQueryable<Location>> GetOrganizationLocations(int organizationId)
    {
        var locations = await this.GetMultipleAsync();
        return locations.Where(l => l.OrganizationId == organizationId);
    }
}