using Microsoft.AspNetCore.Mvc;
using ArtBooking.Model;
using ArtBooking.Application;
using Microsoft.AspNetCore.Authorization;
using ArtBooking.Identity;
namespace ArtBooking.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IdentityService _identity;

    public UserController(IdentityService identity)
    {
        _identity = identity;
    }

    /// <summary>
    /// Gets list of all organizations
    /// </summary>
    /// <returns></returns>
    [HttpGet("info")]
    [Authorize]
    public User Info()
    {
        return _identity.User;
    }

}
