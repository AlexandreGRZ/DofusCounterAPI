using DofusAPI.Services;
using DofusEquipementFetcher.Data;
using Microsoft.AspNetCore.Mvc;

namespace DofusAPI.Controllers;

[ApiController]
[Route("DofusAPI/v1/equipments")]
public class EquipmentController : ControllerBase
{
    private readonly EquipmentService _equipmentService;

    public EquipmentController(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Equipment>>> GetAll()
    {
        return await _equipmentService.GetAll();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Equipment>> GetById(string id)
    {
        var equipment = await _equipmentService.GetById(id);
        if (equipment == null)
        {
            return NotFound();
        }
        return equipment;
    }
    
    [HttpGet("by-ankama/{ankamaId}")]
    public async Task<ActionResult<Equipment>> GetByAnkamaId(int ankamaId)
    {
        var equipment = await _equipmentService.GetByAnkamaId(ankamaId);
        if (equipment == null)
        {
            return NotFound("Équipement non trouvé.");
        }
        return Ok(equipment);
    }

    
    [HttpGet("paged")]
    public async Task<ActionResult<List<Equipment>>> GetEquipments(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null)
    {
        var equipments = await _equipmentService.GetPaged(page, pageSize, search);
        return Ok(equipments);
    }


    [HttpGet("level/{level}")]
    public async Task<ActionResult<List<Equipment>>> GetByLevel(int level)
    {
        return await _equipmentService.GetByLevel(level);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Equipment equipment)
    {
        await _equipmentService.Create(equipment);
        return CreatedAtAction(nameof(GetById), new { id = equipment.AnkamaId }, equipment);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _equipmentService.Delete(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
