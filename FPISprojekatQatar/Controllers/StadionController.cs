using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces; // Dodajte putanju do vašeg interfejsa
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilnoQatarBack.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class StadionController : ControllerBase
    {
        private readonly IStadionService _stadionService;

        public StadionController(IStadionService stadionService)
        {
            _stadionService = stadionService;
        }

        // GET: api/Stadion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StadionDTO>>> GetStadioni()
        {
            var stadioniDTO = await _stadionService.GetAllStadioniAsync();
            return Ok(stadioniDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StadionDTO>> GetStadion(int id)
        {
            var stadionDTO = await _stadionService.GetStadionByIdAsync(id);
            if (stadionDTO == null)
                return NotFound();
            return Ok(stadionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StadionDTO>> CreateStadion([FromBody] StadionDTO stadionDTO)
        {
            var createdStadion = await _stadionService.CreateStadionAsync(stadionDTO);
            return CreatedAtAction(nameof(GetStadion), new { id = createdStadion.Id }, createdStadion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStadion(int id, [FromBody] StadionDTO stadionDTO)
        {
            var success = await _stadionService.UpdateStadionAsync(id, stadionDTO);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStadion(int id)
        {
            var success = await _stadionService.DeleteStadionAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
