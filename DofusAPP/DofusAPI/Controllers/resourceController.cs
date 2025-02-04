using DofusAPI.Services;
using DofusData.PriceData;
using DofusResourceFetcher.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DofusAPI.Controllers;

[ApiController]
[Route("DofusAPI/v1/resources")]
public class ResourceController : ControllerBase
{
    private readonly ResourceService _resourceService;

    public ResourceController(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Resource>>> GetAll()
    {
        return await _resourceService.GetAll();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Resource>> GetById(string id)
    {
        var resource = await _resourceService.GetById(id);
        if (resource == null)
        {
            return NotFound();
        }
        return resource;
    }

    [HttpGet("level/{level}")]
    public async Task<ActionResult<List<Resource>>> GetByLevel(int level)
    {
        return await _resourceService.GetByLevel(level);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Resource resource)
    {
        await _resourceService.Create(resource);
        return CreatedAtAction(nameof(GetById), new { id = resource.AnkamaId }, resource);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _resourceService.Delete(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

}