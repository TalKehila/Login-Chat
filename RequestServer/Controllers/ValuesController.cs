using Microsoft.AspNetCore.Mvc;
//using RequestServer.Service;
namespace RequestServer.Controllers

{
    [Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		//private readonly PingService _pingService;

		//public ValuesController(PingService pingService)
		//{
		//	_pingService = pingService;
		//}
		//[HttpGet("{Username,Token}")]
		//public void UpdateUserOnline([FromBody] string Username, string Token)
		//{
		//	Users.AddUser(Username, Token);
		//}


		[HttpPost("UpdateUserOnline")]
		public IActionResult UpdateUserOnline([FromBody] string Username, string Token)
		{
			Users.AddUser(Username, Token);
			return Ok("User updated online.");
		}


		[HttpGet("{Username}")]
		public IActionResult UpdateUserOffline([FromBody] string Username)
		{
			Users.DelUser(Username);
			return Ok("user updated online");
		}

		[HttpGet("{Username}/{Tokenq}")]
		public bool ValidateUser([FromBody] string Username, string Tokenq)
		{
			return Users.ValidateUser(Username, Tokenq);	
		}

		[HttpGet]
		public List<KeyValuePair<string, string>> GetOnlineUsers()
		{
			return Users._users;
		}

		//[HttpPost("SendPing")]
		//public async Task<IActionResult> SendPing([FromBody] SendPingRequest request)
		//{
		//	await _pingService.SendPing(request.Username, request.Token, request.Address, request.Timeout);
		//	return Ok("Ping sent successfully");
		//}

	}
}
//[HttpGet]
//public IEnumerable<string> Get()
//{
//	return new string[] { "value1", "value2" };
//}

//// GET api/<ValuesController>/5
//[HttpGet("{id}")]
//public string Get(int id)
//{
//	return "value";
//}

//// POST api/<ValuesController>
//[HttpPost]
//public void Post([FromBody] string value)
//{
//}

//// PUT api/<ValuesController>/5
//[HttpPut("{id}")]
//public void Put(int id, [FromBody] string value)
//{
//}

//// DELETE api/<ValuesController>/5
//[HttpDelete("{id}")]
//public void Delete(int id)
//{
//}