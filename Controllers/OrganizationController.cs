using Microsoft.AspNetCore.Mvc;
using ArtBooking.Model;
using ArtBooking.Storage;
using ArtBooking.Application;
namespace ArtBooking.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationSerivce _organizations;

    public OrganizationController(IOrganizationSerivce organizations)
    {
        _organizations = organizations;
    }

    // private List<Organization> organizationMockup = new List<Organization>() {
    //     new Organization() {
    //         OrganizationId = 1,
    //         OrganizationName = "Teatr Bagatela",
    //         // Karmelicka 6, 31-128 Kraków
    //         Address = new Address() {
    //             Street = "Karmelicka",
    //             AddressNumber = "6",
    //             Town = "Kraków",
    //             PostalCode = "31-128"
    //         }
    //     },
    //     new Organization() {
    //         OrganizationId = 2,
    //         OrganizationName = "Kino Kijów",
    //         // Aleja Zygmunta Krasińskiego 34, 30-101 Kraków
    //         Address = new Address() {
    //             Street = "Aleja Zygmunta Krasińskiego",
    //             AddressNumber = "34",
    //             Town = "Kraków",
    //             PostalCode = "30-101"
    //         }
    //     }
    // };

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
}
