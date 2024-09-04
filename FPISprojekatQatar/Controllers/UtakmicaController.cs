using Domain.Model;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MobilnoQatarBack.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class UtakmicaController : ControllerBase
    {
        private readonly IUtakmicaService _utakmicaService;

        public UtakmicaController(IUtakmicaService utakmicaService)
        {
            _utakmicaService = utakmicaService;
        }

        // GET: api/Utakmica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtakmicaDTO>>> GetUtakmice()
        {
            var utakmiceDTO = await _utakmicaService.GetAllUtakmiceAsync();
            return Ok(utakmiceDTO);
        }

        // GET: api/Utakmica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtakmicaDTO>> GetUtakmica(int id)
        {
            var utakmicaDTO = await _utakmicaService.GetUtakmicaByIdAsync(id);
            if (utakmicaDTO == null)
            {
                return NotFound();
            }
            return Ok(utakmicaDTO);
        }

        // POST: api/Utakmica
        [HttpPost]
        public async Task<IActionResult> PostUtakmica([FromBody] UtakmicaDTO utakmicaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUtakmica = await _utakmicaService.CreateUtakmicaAsync(utakmicaDTO);
                return CreatedAtAction(nameof(GetUtakmica), new { id = createdUtakmica.Id }, createdUtakmica);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Utakmica/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtakmica(int id, UtakmicaDTO utakmicaDTO)
        {
            if (!await _utakmicaService.UpdateUtakmicaAsync(id, utakmicaDTO))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Utakmica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtakmica(int id)
        {
            if (!await _utakmicaService.DeleteUtakmicaAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }


        // PUT: api/Utakmica/5/SetRezultat
        [HttpPut("{id}/SetRezultat")]
        public async Task<IActionResult> SetRezultatUtakmice(int id, [FromBody] RezultatDTO rezultatDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _utakmicaService.SetRezultatUtakmiceAsync(id, rezultatDTO.Tim1Golovi, rezultatDTO.Tim2Golovi);
            if (!updateResult)
            {
                return NotFound(new { Message = "Utakmica nije pronađena ili ažuriranje nije uspelo." });
            }

            return NoContent(); // Uspešno ažuriran odgovor
        }


        [HttpGet("ByGroup/{groupId}")]
        public async Task<IActionResult> GetMatchesByGroupId(int groupId)
        {
            var matches = await _utakmicaService.GetUtakmiceByGroupIdAsync(groupId);
            if (matches == null || !matches.Any())
                return NotFound("No matches found for the given group.");

            return Ok(matches);
        }

        // DELETE: api/Utakmica/DeleteAll
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllUtakmice()
        {
            var result = await _utakmicaService.DeleteAllUtakmiceAsync();
            if (!result)
            {
                return NotFound("No matches found to delete.");
            }
            return NoContent();
        }


    }
}
