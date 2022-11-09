using CompWeek.Api.Domain.Interfaces;
using CompWeek.Domain.Commons;
using CompWeek.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompWeek.Api.Controllers;

[ApiController]
[Route("v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IUnitOfService _services;

    public AuthenticationController(IUnitOfService services)
    {
        _services = services;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        try
        {
            var result = await _services.AuthenticationService.Authenticate(request);

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }
}