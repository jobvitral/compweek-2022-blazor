using CompWeek.Api.Domain.Interfaces;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompWeek.Api.Controllers;

[ApiController]
[Route("v1/roles")]
public class RoleController : ControllerBase
{
    private readonly IUnitOfService _services;

    public RoleController(IUnitOfService services)
    {
        _services = services;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _services.RoleService.Get(id);

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
    public async Task<IActionResult> Get([FromBody] RoleFilter filter)
    {
        try
        {
            var result = await _services.RoleService.Get(filter);
            
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
    public async Task<IActionResult> Insert([FromBody] Role item)
    {
        try
        {
            var result = await _services.RoleService.Insert(item);

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
    public async Task<IActionResult> Update([FromBody] Role item)
    {
        try
        {
            var result = await _services.RoleService.Update(item);

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
            var result = await _services.RoleService.Delete(id);

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