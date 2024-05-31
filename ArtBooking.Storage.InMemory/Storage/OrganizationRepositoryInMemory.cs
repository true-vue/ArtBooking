namespace ArtBooking.Storage;
using ArtBooking.Model;

public class OrganizationRepositoryInMemory : IOrganizationRepository
{
    private readonly ArtBookingDbContext _context;

    public OrganizationRepositoryInMemory(ArtBookingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all organizations and allows further filtering.
    /// </summary>
    /// <returns></returns>
    public async Task<IQueryable<Organization>> GetMultipleAsync()
    {
        return await Task.FromResult(_context.Organizations);
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await Task.FromResult(_context.Organizations.Find(id));
    }

    public async Task<Organization> AddAsync(Organization organization)
    {
        await _context.Organizations.AddAsync(organization);
        return organization;
    }

    public async Task<Organization> UpdateAsync(Organization organization)
    {
        var existingOrganization = _context.Locations.Find(organization.OrganizationId);
        if (existingOrganization != null)
        {
            _context.Entry(existingOrganization).CurrentValues.SetValues(organization);
        }

        return await Task.Run(() =>
        {
            return organization;
        });
    }

    public async Task DeleteAsync(int id)
    {
        var removedOrganization = _context.Organizations.Find(id);
        if (removedOrganization == null)
        {
            throw new InvalidOperationException($"Organization with id:{id} cannot be found in storage.");
        }
        await Task.Run(() =>
        {
            _context.Organizations.Remove(removedOrganization);
        });
    }
}