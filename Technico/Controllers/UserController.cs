using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<UserFullDTO?>> GetById(Guid id)
        {
            return await _userService.GetAsync(id);
        }

        // api/User/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] UserFullDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest("Mismatched user ID");
            }

            var result = await _userService.UpdateAsync(id,user);

            if (!result.Success)
            {
                return Conflict(new { message = result.Message });
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<UserSimpleDTO>> PostUser(UserFullDTO user)
        {
            var result = await _userService.CreateAsync(user);

            if (!result.Success)
            {
                return Conflict(new { message = result.Message });
            }

            return CreatedAtAction(nameof(PostUser), new { id = result.Data?.Id }, result.Data);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleteResult = await _userService.DeleteAsync(id);

            if (!deleteResult.Success)
            {
                return BadRequest(new { message = deleteResult.Message });
            }

            return Ok(new { message = deleteResult.Message });
        }


        // Login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            Console.WriteLine($"Received Email: {loginDto.Email}, Password: {loginDto.Password}");
            var result = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            if (!result.Success)
            {
                Console.WriteLine("Login failed: " + result.Message);
                return Unauthorized(new { message = result.Message });
            }

            Console.WriteLine("Login successful, returning token.");
            return Ok(result);
        }


        //Response.Cookies.Append("token", result.Token, new CookieOptions
        //{
        //    HttpOnly = true,
        //    SameSite = SameSiteMode.Lax,
        //    Expires = DateTime.UtcNow.AddDays(1),
        //    Path = "/"
        //});

        [HttpGet("owners")]
        public async Task<ActionResult<List<UserFullDTO>>> GetOwners()
        {
            return await _userService.GetOwnersAsync();
        }

    }
}
