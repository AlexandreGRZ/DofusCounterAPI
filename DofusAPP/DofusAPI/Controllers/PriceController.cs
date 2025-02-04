using DofusAPI.Services;
using DofusData.PriceData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DofusAPI.Controllers
{
    [ApiController]
    [Route("DofusAPI/v1/prices")]
    public class PriceController : ControllerBase
    {
        private readonly PriceService _priceService;

        public PriceController(PriceService priceService)
        {
            _priceService = priceService;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddPrice([FromBody] Price price)
        {
            if (price == null)
                return BadRequest("Le prix ne peut pas être vide.");
        
            await _priceService.UpsertPrice(price.AnkamaId, price.Value);
            return CreatedAtAction(nameof(GetByItem), new { ankamaId = price.AnkamaId }, price);
        }

        [HttpGet("{ankamaId}")]
        public async Task<ActionResult<List<Price>>> GetByItem(int ankamaId)
        {
            var prices = await _priceService.GetByItem(ankamaId);
            if (prices.Count == 0)
                return NotFound("Aucun prix trouvé pour cet item.");

            return prices;
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceUpdateRequest request)
        {
            if (request == null || request.AnkamaId <= 0 || request.Value < 0)
                return BadRequest("Données invalides.");

            var updated = await _priceService.UpsertPrice(request.AnkamaId, request.Value);

            if (!updated)
                return NotFound($"Aucune ressource trouvée avec l'ID {request.AnkamaId}");

            return Ok(new { message = $"Prix mis à jour pour la ressource {request.AnkamaId}" });
        }
    }
}

