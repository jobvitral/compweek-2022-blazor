using CompWeek.Api.Domain.Interfaces;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompWeek.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly IUnitOfService _services;

    public UserController(IUnitOfService services)
    {
        _services = services;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _services.UserService.Get(id);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpPost("filter")]
    public async Task<IActionResult> Get([FromBody] UserFilter filter)
    {
        try
        {
            var result = await _services.UserService.Get(filter);

            if(!result.Any())
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] User item)
    {
        try
        {
            var result = await _services.UserService.Insert(item);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] User item)
    {
        try
        {
            var result = await _services.UserService.Update(item);

            if(result == null)
                return NoContent();

            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new { e.Errors });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _services.UserService.Delete(id);

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