using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Technico.Context;
using Technico.Models;
using Microsoft.Extensions.Hosting;
using Technico.Services;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IService<Owner, Guid> _ownerService;

        public OwnerController(IService<Owner, Guid> ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<List<Owner?>>> GetAll()
        {
            return await _ownerService.GetAllAsync();
        }

        // GET: api/Owner/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetById(Guid id)
        {
            var owner = await _ownerService.GetAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // PUT: api/Owner/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

           // _context.Entry(post).State = EntityState.Modified;

            var updatedOwner = await _ownerService.UpdateAsync(owner);
            if (updatedOwner == null) {
                return NotFound();
            }

            return Ok(updatedOwner);

        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            
            var newOwner = await _ownerService.CreateAsync(owner);

            return CreatedAtAction("GetPost", new { id = newOwner.Id}, owner);
        }

        // DELETE: api/Owner/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var owner = await _ownerService.DeleteAsync(id);
            if (!owner)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
