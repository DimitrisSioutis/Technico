using Microsoft.AspNetCore.Mvc;
using Technico.Models;
using Technico.Services;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<List<Property?>>> GetAll()
        {
            return await _propertyService.GetAllAsync();
        }

        // GET: api/Property/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetById(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return property;
        }

        // POST: api/Property
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
            var newProperty = await _propertyService.CreateAsync(property);
            return CreatedAtAction("GetById", new { id = newProperty.PropertyIDNumber }, newProperty);
        }

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, Property property)
        {
            if (id != property.PropertyIDNumber)
            {
                return BadRequest();
            }

            var updatedProperty = await _propertyService.UpdateAsync(property);
            if (updatedProperty == null)
            {
                return NotFound();
            }

            return Ok(updatedProperty);
        }

        // DELETE: api/Property/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var result = await _propertyService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
