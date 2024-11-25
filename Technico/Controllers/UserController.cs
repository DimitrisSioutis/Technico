using Microsoft.AspNetCore.Mvc;
using Technico.Services;
using Technico.Dtos;
using Technico.Interfaces;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<UserSimpleDTO>>> GetAll()
        {
            return await _userService.GetAllAsync();
        }



        // GET: api/User/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFullDTO>> GetById(Guid id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // api/User/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] UserFullDTO user)
        {
            // Ensure the provided id matches the user's id (optional validation)
            if (id != user.Id)
            {
                return BadRequest("Mismatched user ID");
            }

            // Call the service to update the user
            var updatedUser = await _userService.UpdateAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }


        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserSimpleDTO>> PostUser(UserFullDTO user)
        {
            var newUser = await _userService.CreateAsync(user);

            if (newUser == null)
            {
                return BadRequest(new { message = "VAT Number or Email already exists." });
            }

            return CreatedAtAction(nameof(PostUser), new { id = newUser.Id }, newUser);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var owner = await _userService.DeleteAsync(id);
            if (!owner)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
