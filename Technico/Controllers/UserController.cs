using Microsoft.AspNetCore.Mvc;
using Technico.Dtos;
using Technico.Interfaces;
using static Technico.Services.UserService;

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



        [HttpGet("{id}")]
        public async Task<ActionResult<UserFullDTO?>> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserFullDTO user)
        {
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, user);
                return Ok(updatedUser); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update user"); // Handle any other error
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { message = "Invalid data", errors });
            }

            try
            {
                var userDto = await _userService.CreateAsync(createDto);
                return Ok(userDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
   
            var token = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { token });
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
