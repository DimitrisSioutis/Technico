using Microsoft.AspNetCore.Mvc;
using Technico.Models;
using Technico.Services;
using Technico.Dtos;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly RepairService _repairService;

        public RepairController(RepairService repairService)
        {
            _repairService = repairService;
        }

        // GET: api/Repair
        [HttpGet]
        public async Task<ActionResult<List<Repair?>>> GetAll()
        {
            return await _repairService.GetAllAsync();
        }

        // GET: api/Repair/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> GetById(Guid id)
        {
            var repair = await _repairService.GetAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            return repair;
        }

        [HttpPost]
        public async Task<ActionResult<RepairDTO?>> PostRepair(RepairDTO repair)
        {
            var newRepair = await _repairService.CreateAsync(repair);
            if (newRepair == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetById", new { id = newRepair.Id }, newRepair);
        }


        // PUT: api/Repair/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRepair([FromRoute] Guid id, [FromBody] RepairDTO repair)
        {
            if (id != repair.Id) 
            {
                return BadRequest();
            }

            var updatedRepair = await _repairService.UpdateAsync(id,repair);
            if (updatedRepair == null)
            {
                return NotFound();
            }

            return Ok(updatedRepair);
        }

        // DELETE: api/Repair/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepair(Guid id)
        {
            var result = await _repairService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
