using Microsoft.AspNetCore.Mvc;
using Technico.Interfaces;
using Technico.Dtos;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<List<SimplePropertyDTO?>>> GetAll()
        {
            return await _propertyService.GetAllAsync();
        }

        // GET: api/Property/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDTO>> GetById(Guid id)
        {
            var property = await _propertyService.GetAsync(id);
            if (property == null) return NotFound();
            return property;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyCreateDTO propertyDTO)
        {
            var result = await _propertyService.CreateAsync(propertyDTO);

            if (!result.Success)
                return Conflict(new { message = "A property with this address already exists." });

            return CreatedAtAction("GetById", new { id = result.Data!.PropertyId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] PropertyDTO propertyDTO)
        {
            if (id != propertyDTO.PropertyId)
            {
                return BadRequest("Mismatched Property ID");
            }
            var updatedProperty = await _propertyService.UpdateAsync(id, propertyDTO);
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
