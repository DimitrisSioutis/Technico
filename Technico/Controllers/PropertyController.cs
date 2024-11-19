using Microsoft.AspNetCore.Mvc;
using Technico.Services;
using Technico.Dtos;

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
        public async Task<ActionResult<List<PropertyDTO?>>> GetAll()
        {
            return await _propertyService.GetAllAsync();
        }

        // GET: api/Property/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDTO>> GetById(Guid id)
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
        public async Task<ActionResult<PropertyDTO>> PostProperty(PropertyDTO propertyDTO)
        {
            var newProperty = await _propertyService.CreateAsync(propertyDTO);
            return CreatedAtAction("GetById", new { id = newProperty.PropertyIDNumber }, newProperty);
        }

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, PropertyDTO property)
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
