using Microsoft.AspNetCore.Mvc;
using ArtBooking.Model;
using ArtBooking.Application;
namespace ArtBooking.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizations;
    private readonly IArtBookingStorageContext _storage;

    public OrganizationController(IOrganizationService organizations, IArtBookingStorageContext storage)
    {
        _organizations = organizations;
        _storage = storage;
    }

    /// <summary>
    /// Gets list of all organizations
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IEnumerable<Organization>> GetAll()
    {
        return await _organizations.GetMultipleAsync();
    }

    /// <summary>
    /// Gets organization by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get/{id}")]
    public async Task<ActionResult<Organization>> Get(int id)
    {
        var organization = await _organizations.GetAsync(id);

        if (organization == null) return NotFound();

        return organization;
    }


    [HttpPost("save")]
    public async Task<ActionResult<Organization>> Save(Organization editedOrganization)
    {
        var isCreation = editedOrganization.OrganizationId == 0;
        var savedOrganization = await _organizations.SaveAsync(editedOrganization);

        _storage.SaveChanges();

        if (isCreation)
        {
            return Created($"/api/organization/get/{savedOrganization.OrganizationId}", savedOrganization);
        }
        return Ok(savedOrganization);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> Delete(int id)
    {
        await _organizations.DeleteAsync(id);
        await _storage.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("location/add")]
    public async Task<ActionResult<Location>> AddNewLocationAsync(Location newLocation)
    {
        var result = await _organizations.AddNewLocationAsync(newLocation);
        await _storage.SaveChangesAsync();

        if (result.Success)
        {
            return result.Data as Location;
        }

        return StatusCode((int)result.HttpStatusCode, result.HttpData(true));
    }

    [HttpDelete("location/delete")]
    public async Task<ActionResult<Location>> DeleteLocationAsync(int locationId)
    {
        var result = await _organizations.DeleteLocationAsync(locationId);
        await _storage.SaveChangesAsync();

        if (result.Success)
        {
            return result.Data as Location;
        }

        return StatusCode((int)result.HttpStatusCode, result.HttpData(true));
    }
}
