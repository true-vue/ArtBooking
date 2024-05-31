namespace ArtBooking.Application;
using ArtBooking.Model;
using ArtBooking.Storage;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizations;
    private readonly ILocationRepository _locations;

    private readonly IArtBookingStorageContext _dbContext;

    public OrganizationService(IOrganizationRepository organizationRepository, ILocationRepository locations, IArtBookingStorageContext dbContext)
    {
        _organizations = organizationRepository;
        _locations = locations;
        _dbContext = dbContext;
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
        if (await IsUniqueOrganization(organization))
        {
            if (organization.OrganizationId == 0)
            {
                await _organizations.AddAsync(organization);
            }
            else
            {
                await _organizations.UpdateAsync(organization);
            }
        }
        else
        {
            throw new InvalidOperationException($"Organization with name '{organization.OrganizationName}' already exists");
        }

        return organization;
    }

    public async Task DeleteAsync(int id)
    {
        await _organizations.DeleteAsync(id);
    }

    public async Task<OperationResult<dynamic>> AddNewLocationAsync(Location item)
    {
        // Data consitency check (detemine whether organization specified in location exist) - that would normally be handled in ifrastructure layer. 
        // Since we are not sure whether underlaying storege will guarantee this check it is implemented here.
        var targetOrganization = await GetAsync(item.OrganizationId);
        if (targetOrganization == null) return OperationResult<dynamic>.Fail(OperationResultErrorCodes.LocationOrganizationNotFound, item);

        // Duplication check - race condition might occur so it would be good idea to put lock or use transaction in inferastructure level.
        if (!await IsUniqueLocation(item)) return OperationResult<dynamic>.Fail(OperationResultErrorCodes.DuplicationOccured, item);

        try
        {
            var newLocation = await _locations.AddAsync(item);

            return OperationResult<dynamic>.Ok(newLocation);

        }
        catch (Exception exp)
        {
            return OperationResult<dynamic>.Fail(OperationResultErrorCodes.OperationFailedInInnerLayer, exp);
        }
        // throw new InvalidOperationException($"Location with name {item.LocationName} is already present in organization");
    }

    public async Task<OperationResult<dynamic>> DeleteLocationAsync(int locationId)
    {
        try
        {
            await _locations.DeleteAsync(locationId);

            return OperationResult<dynamic>.Ok(null);

        }
        catch (Exception exp)
        {
            return OperationResult<dynamic>.Fail(OperationResultErrorCodes.OperationFailedInInnerLayer, exp);
        }
        // throw new InvalidOperationException($"Location with name {item.LocationName} is already present in organization");
    }

    public async Task<OperationResult<dynamic>> GetOrganizationLocationsAsync(int organizationId)
    {
        try
        {
            var locations = await _locations.GetOrganizationLocations(organizationId);
            return OperationResult<dynamic>.Ok(locations);
        }
        catch (Exception exp)
        {
            return OperationResult<dynamic>.Fail(OperationResultErrorCodes.OperationFailedInInnerLayer, exp);
        }
    }

    private async Task<bool> IsUniqueLocation(Location item)
    {
        var locations = await _locations.GetOrganizationLocations(item.OrganizationId);
        return !locations.Any(l => l.LocationName.Trim().ToLower() == item.LocationName.Trim().ToLower());
    }

    private async Task<bool> IsUniqueOrganization(Organization item)
    {
        var organizations = await this.GetMultipleAsync();
        var isUnique = !organizations.Any(o => o.OrganizationName.Equals(item.OrganizationName, StringComparison.InvariantCultureIgnoreCase) && o.OrganizationId != item.OrganizationId);
        return isUnique;
    }
}