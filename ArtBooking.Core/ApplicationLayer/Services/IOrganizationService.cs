namespace ArtBooking.Application;
using ArtBooking.Model;
using ArtBooking.Storage;

public interface IOrganizationService : IServiceBase<Organization>
{
    Task<OperationResult<dynamic>> AddNewLocationAsync(Location item);

    Task<OperationResult<dynamic>> DeleteLocationAsync(int locationId);
}