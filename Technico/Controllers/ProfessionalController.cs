using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Technico.Context;
using Technico.Models;
using Microsoft.Extensions.Hosting;
using Technico.Services;
using Technico.Repositories;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IService<Professional, long> _professionalService;

        public ProfessionalController(IService<Professional, long> professionalService)
        {
            _professionalService = professionalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Professional?>>> GetAll()
        {
            return await _professionalService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professional>> GetById(long id)
        {
            var owner = await _professionalService.GetAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }


        [HttpPut("{vat}")]
        public async Task<IActionResult> PutPost([FromRoute] string vat, [FromBody] Professional pro)
        {
            if (vat != pro.VATNumber)
            {
                return BadRequest();
            }

            // _context.Entry(post).State = EntityState.Modified;

            var updatedPro = await _professionalService.UpdateAsync(pro);
            if (updatedPro== null)
            {
                return NotFound();
            }

            return Ok(updatedPro);

        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<Professional>> PostOwner(Professional pro)
        {

            var newOwner = await _professionalService.CreateAsync(pro);

            return CreatedAtAction("GetPost", new { vat = pro.VATNumber }, pro);
        }

        // DELETE: api/Owner/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            var owner = await _professionalService.DeleteAsync(id);
            if (!owner)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
