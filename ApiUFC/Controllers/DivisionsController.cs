using ApiUFC.Interfaces;
using ApiUFC.Models;
using ApiUFC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiUFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionsController : ControllerBase
    {
        private readonly IDivisionsService _divisionsService;

        public DivisionsController(IDivisionsService divisionsService)
        {
            _divisionsService = divisionsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Division>>> GetDivisions()
        {
            var divisions = await _divisionsService.GetDivisions();
            return Ok(divisions);
        }

        [HttpGet("{divisionId}")]
        public async Task<ActionResult<Division>> GetDivision(int divisionId)
        {
            var division = await _divisionsService.GetDivision(divisionId);

            if (division == null)
                return NotFound();

            return Ok(division);
        }

        // ⭐ POST /api/divisions
        [HttpPost]
        public async Task<ActionResult> CreateDivision(Division division)
        {
            await _divisionsService.CreateDivision(division);
            return CreatedAtAction(nameof(GetDivision), new { divisionId = division.Id }, division);
        }

        [HttpPut("{divisionId}")]
        public async Task<ActionResult> UpdateDivision(int divisionId, Division division)
        {
            var updated = await _divisionsService.UpdateDivision(divisionId, division);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{divisionId}")]
        public async Task<ActionResult> DeleteDivision(int divisionId)
        {
            var existing = await _divisionsService.GetDivision(divisionId);
            if (existing == null)
                return NotFound();

            await _divisionsService.DeleteDivision(divisionId);
            return NoContent();
        }

    }
}
