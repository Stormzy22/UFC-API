using ApiUFC.Interfaces;
using ApiUFC.Models;
using ApiUFC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationsController : ControllerBase
    {
        private readonly INationsService _nationsService;

        public NationsController(INationsService nationsService)
        {
            _nationsService = nationsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Nation>>> GetNations()
        {
            var nations = await _nationsService.GetNations();
            return Ok(nations);
        }

        [HttpGet("{nationId}")]
        public async Task<ActionResult<Nation>> GetNation(int nationId)
        {
            var nation = await _nationsService.GetNation(nationId);

            if (nation == null)
                return NotFound();

            return Ok(nation);
        }

        // ⭐ POST /api/nations
        [HttpPost]
        public async Task<ActionResult> CreateNation(Nation nation)
        {
            await _nationsService.CreateNation(nation);
            return CreatedAtAction(nameof(GetNation), new { nationId = nation.Id }, nation);
        }

        [HttpPut("{nationId}")]
        public async Task<ActionResult> UpdateNation(int nationId, Nation nation)
        {
            var updated = await _nationsService.UpdateNation(nationId, nation);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{nationId}")]
        public async Task<ActionResult> DeleteNation(int nationId)
        {
            var existing = await _nationsService.GetNation(nationId);
            if (existing == null)
                return NotFound();

            await _nationsService.DeleteNation(nationId);
            return NoContent();
        }

    }
}
