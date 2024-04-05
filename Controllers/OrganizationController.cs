using Microsoft.AspNetCore.Mvc;
using ArtBooking.Model;
using ArtBooking.Storage;
using ArtBooking.Application;
using Microsoft.AspNetCore.SignalR.Protocol;
namespace ArtBooking.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizations;

    public OrganizationController(IOrganizationService organizations)
    {
        _organizations = organizations;
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

        if (isCreation)
        {
            return Created($"/api/organization/get/{savedOrganization.OrganizationId}", savedOrganization);
        }
        return Ok(savedOrganization);
    }

    [HttpDelete("/delete")]
    public ActionResult Delete(int id)
    {
        return Ok();
    }

    [HttpPost("/location/add")]
    public async Task<ActionResult<Location>> AddNewLocationAsync(Location newLocation)
    {
        var result = await _organizations.AddNewLocationAsync(newLocation);

        if (result.Success)
        {
            return result.Data as Location;
        }

        return StatusCode((int)result.HttpStatusCode, result.HttpData(true));
    }
}
