namespace ArtBooking.Storage.Mockup;
using ArtBooking.Model;

public class OrganizationRepositoryMockup : IOrganizationRepository
{
    private readonly ArtBookingDataMockup _context;

    public OrganizationRepositoryMockup(ArtBookingDataMockup context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all organizations and allows further filtering.
    /// </summary>
    /// <returns></returns>
    public async Task<IQueryable<Organization>> GetMultipleAsync()
    {
        return await Task.Run(() => _context.Organizations.AsQueryable());
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await Task.Run(() => _context.Organizations.Find(o => o.OrganizationId == id));
    }

    public async Task<Organization> AddAsync(Organization organization)
    {
        return await Task.Run(() => _context.Organizations.Add(organization));
    }

    public async Task<Organization> UpdateAsync(Organization organization)
    {
        var id = organization.OrganizationId;
        return await Task.Run(() =>
        {
            var storedOrganizationIdx = _context.Organizations.FindIndex(o => o.OrganizationId == id);
            if (storedOrganizationIdx == -1)
            {
                throw new InvalidOperationException($"Organization with id:{id} cannot be found in storage.");
            }
            _context.Organizations[storedOrganizationIdx] = organization;

            return organization;
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
}