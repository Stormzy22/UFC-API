using ApiUFC.Interfaces;
using ApiUFC.Models;
using ApiUFC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FightersController : ControllerBase
    {
        private readonly IFightersService _fightersService;

        public FightersController(IFightersService fightersService)
        {
            _fightersService = fightersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fighter>>> GetFighters()
        {
            var fighters = await _fightersService.GetFighters();
            return Ok(fighters);
        }

        [HttpGet("{fighterId}")]
        public async Task<ActionResult<Fighter>> GetFighter(int fighterId)
        {
            var fighter = await _fightersService.GetFighter(fighterId);

            if (fighter == null)
                return NotFound();

            return Ok(fighter);
        }

        // ⭐ POST /api/fighters
        [HttpPost]
        public async Task<ActionResult> CreateFighter(Fighter fighter)
        {
            await _fightersService.CreateFighter(fighter);
            return CreatedAtAction(nameof(GetFighter), new { fighterId = fighter.Id }, fighter);
        }

        // ⭐ PUT /api/fighters/{id}
        [HttpPut("{fighterId}")]
        public async Task<ActionResult> UpdateFighter(int fighterId, Fighter fighter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _fightersService.UpdateFighter(fighterId, fighter);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // ⭐ DELETE /api/fighters/{id}
        [HttpDelete("{fighterId}")]
        public async Task<ActionResult> DeleteFighter(int fighterId)
        {
            var existing = await _fightersService.GetFighter(fighterId);
            if (existing == null)
                return NotFound();

            await _fightersService.DeleteFighter(fighterId);
            return NoContent();
        }
    }
}
