namespace ArtBooking.Storage.InMemory;
using ArtBooking.Model;

public class LocationRepositoryInMemory : ILocationRepository
{
    private readonly ArtBookingDbContext _context;

    public LocationRepositoryInMemory(ArtBookingDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Location>> GetMultipleAsync()
    {
        return await Task.Run(() => _context.Locations);
    }

    public async Task<Location?> GetAsync(int id)
    {
        return await Task.Run(() => _context.Locations.Find(id));
    }

    public async Task<Location> AddAsync(Location location)
    {
        await _context.Locations.AddAsync(location);
        return location;
    }

    public async Task<Location> UpdateAsync(Location location)
    {
        var existingLocation = _context.Locations.Find(location.LocationId);
        if (existingLocation != null)
        {
            _context.Entry(existingLocation).CurrentValues.SetValues(location);
        }

        return await Task.Run(() =>
        {
            return location;
        });
    }

    public async Task DeleteAsync(int id)
    {
        var removedLocation = _context.Locations.Find(id);
        if (removedLocation == null)
        {
            throw new InvalidOperationException($"Location with id:{id} cannot be found in storage.");
        }
        await Task.Run(() =>
        {
            _context.Locations.Remove(removedLocation);
        });
    }

    public async Task<IQueryable<Location>> GetOrganizationLocations(int organizationId)
    {
        var locations = await this.GetMultipleAsync();
        return locations.Where(l => l.OrganizationId == organizationId);
    }
}