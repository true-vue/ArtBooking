namespace ArtBooking.Storage;

using ArtBooking.Model;

public interface ILocationRepository : IRepositoryBase<Location>
{
    Task<IQueryable<Location>> GetOrganizationLocations(int organizationId);
}