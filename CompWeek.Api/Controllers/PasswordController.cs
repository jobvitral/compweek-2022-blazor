using CompWeek.Api.Domain.Interfaces;
using CompWeek.Domain.Commons;
using CompWeek.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompWeek.Api.Controllers;

[ApiController]
[Route("v1/password")]
public class PasswordController : ControllerBase
{
    private readonly IUnitOfService _services;

    public PasswordController(IUnitOfService services)
    {
        _services = services;
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateRequest item)
    {
        try
        {
            var result = await _services.PasswordService.Update(item);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpPost("recover")]
    public async Task<IActionResult> RequestRecover([FromBody] PasswordRecoverRequest item)
    {
        try
        {
            var result = await _services.PasswordService.RequestRecovery(item);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpPut("recover")]
    public async Task<IActionResult> UpdateRecover([FromBody] PasswordRecoverUpdate item)
    {
        try
        {
            var result = await _services.PasswordService.Recover(item);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }
}