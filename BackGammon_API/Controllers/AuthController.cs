using BackGammon_API.DTO;
using BackGammon_API.Service;
using BackGammon_API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BackGammon_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUser _userService;
		private readonly TokenCheck _tokenCheck;

		public AuthController(IUser userService, TokenCheck tokenCheck)
		{
			_userService = userService;
			_tokenCheck = tokenCheck;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _userService.Register(registerDTO);

			if (result)
			{
				return Ok("Registeration Successfull");
			}
			else
			{
				return BadRequest("Registration Failed");
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginDTO log)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var token = await _userService.Login(log);
			if (token != null && !token.Equals("Invalid Login Action"))
			{
				RequestServer.Controllers.ValuesController vc = new RequestServer.Controllers.ValuesController();
				vc.UpdateUserOnline(log.UserName, token);
				return Ok(token);
			}
			else
			{
				return BadRequest("Invalid Login Action");
			}
		}
		[HttpPost("validate-token")]
		[AllowAnonymous]
		[Authorize]
		public IActionResult ValidateToken()
		{
			string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);

			if (_tokenCheck.ValidationToken(token))
			{
				return Ok(new { Valid = true });
			}
			else
			{
				return Unauthorized(new { Valid = false });
			}
		}

	}
}
