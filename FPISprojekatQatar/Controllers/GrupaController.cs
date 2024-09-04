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

namespace MobilnoQatarBack.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupaDTO>>> GetGrupe()
        {
            var grupeDTO = await _grupaService.GetAllGrupeAsync();
            return Ok(grupeDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupaDTO>> GetGrupa(int id)
        {
            var grupaDTO = await _grupaService.GetGrupaByIdAsync(id);
            if (grupaDTO == null)
                return NotFound();
            return Ok(grupaDTO);
        }

        [HttpPost]
        public async Task<ActionResult<GrupaDTO>> CreateGrupa([FromBody] GrupaDTO grupaDTO)
        {
            var createdGrupa = await _grupaService.CreateGrupaAsync(grupaDTO);
            return CreatedAtAction(nameof(GetGrupa), new { id = createdGrupa.Id }, createdGrupa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupa(int id, [FromBody] GrupaDTO grupaDTO)
        {
            var success = await _grupaService.UpdateGrupaAsync(id, grupaDTO);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupa(int id)
        {
            var success = await _grupaService.DeleteGrupaAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }


        [HttpPost("{grupaId}/addTim/{timId}")]
        public async Task<IActionResult> AddTimToGrupa(int grupaId, int timId)
        {
            var success = await _grupaService.AddTimToGrupaAsync(grupaId, timId);
            if (!success) return BadRequest("Unable to add tim to grupa.");
            return Ok();
        }

        [HttpDelete("removeTim/{timId}")]
        public async Task<IActionResult> RemoveTimFromGrupa(int timId)
        {
            var success = await _grupaService.RemoveTimFromGrupaAsync(timId);
            if (!success) return BadRequest("Unable to remove tim from grupa.");
            return Ok();
        }


        [HttpDelete("deleteAllData")]
        public async Task<IActionResult> DeleteAllData()
        {
            var success = await _grupaService.DeleteAllDataAsync();
            if (!success) return BadRequest("Error during the deletion of all data.");
            return NoContent(); // Successfully deleted all data
        }
    }
}
