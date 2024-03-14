namespace ArtBooking.Application;
using ArtBooking.Model;

public interface IOrganizationSerivce
{
    Task<IQueryable<Organization>> GetMultipleAsync();
    Task<Organization?> GetAsync(int id);
    Task<Organization> SaveAsync(Organization item);
    Task DeleteAsync(int id);
}