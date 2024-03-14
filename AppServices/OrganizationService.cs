namespace ArtBooking.Application;
using ArtBooking.Model;
using ArtBooking.Storage;

public class OrganizationService : IOrganizationSerivce
{
    private readonly IOrganizationRepository _organizations;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizations = organizationRepository;
    }

    public async Task<IQueryable<Organization>> GetMultipleAsync()
    {
        return await _organizations.GetMultipleAsync();
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await _organizations.GetAsync(id);
    }

    public async Task<Organization> SaveAsync(Organization organization)
    {
        if (organization.OrganizationId == 0)
        {
            return await _organizations.AddAsync(organization);
        }
        return await _organizations.UpdateAsync(organization);
    }

    public async Task DeleteAsync(int id)
    {
        await _organizations.DeleteAsync(id);
    }
}