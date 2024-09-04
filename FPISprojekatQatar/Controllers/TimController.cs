using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using MobilnoQatarBack.Data;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MobilnoQatarBack.Services;

namespace MobilnoQatarBack.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class TimController : ControllerBase
    {
        private readonly ITimService _timService;

        public TimController(ITimService timService)
        {
            _timService = timService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimDTO>>> GetTimovi()
        {
            var timoviDTO = await _timService.GetAllTimoviAsync();
            return Ok(timoviDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimDTO>> GetTim(int id)
        {
            var timDTO = await _timService.GetTimByIdAsync(id);
            if (timDTO == null)
                return NotFound();
            return Ok(timDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TimDTO>> CreateTim([FromBody] TimDTO timDTO)
        {
            try
            {
                var createdTim = await _timService.CreateTimAsync(timDTO);
                return CreatedAtAction(nameof(GetTim), new { id = createdTim.Id }, createdTim);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTim(int id, [FromBody] TimDTO timDTO)
        {
            var success = await _timService.UpdateTimAsync(id, timDTO);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTim(int id)
        {
            var success = await _timService.DeleteTimAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpPost("{timId}/addToGrupa/{grupaId}")]
        public async Task<IActionResult> AddTimToGrupa(int timId, int grupaId)
        {
            var success = await _timService.AddTimToGrupaAsync(timId, grupaId);
            if (!success)
                return BadRequest("Unable to add tim to grupa.");
            return Ok();
        }

        [HttpPost("{timId}/removeFromGrupa")]
        public async Task<IActionResult> RemoveTimFromGrupa(int timId)
        {
            var success = await _timService.RemoveTimFromGrupaAsync(timId);
            if (!success)
                return BadRequest("Unable to remove tim from grupa.");
            return Ok();
        }

        [HttpGet("byGrupa/{grupaId}")]
        public async Task<ActionResult<IEnumerable<TimDTO>>> GetTimByGrupaId(int grupaId)
        {
            var timovi = await _timService.GetTimByGrupaIdAsync(grupaId);
            if (timovi == null)
            {
                return NotFound();
            }
            return Ok(timovi);
        }

        // DELETE: api/Utakmica/DeleteAll
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllTimovi()
        {
            var result = await _timService.DeleteAllTimoviAsync();
            if (!result)
            {
                return NotFound("No teams found to delete.");
            }
            return NoContent();
        }


    }
}
