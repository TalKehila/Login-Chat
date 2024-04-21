using BackGammon_API.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackGammon_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersAuthController : ControllerBase
    {
        private readonly IUser _userService;

        public UsersAuthController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return BadRequest();

            return Ok(users);
        }

        [HttpGet("userdata")]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userData = await _userService.GetUserById(userId!);

            if(userData != null)
            {
                return Ok(userData);
            }
            else
            {
                return NotFound();
            }
        }

		
	}
}
