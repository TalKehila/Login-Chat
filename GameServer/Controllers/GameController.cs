using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using RequestServer;
using RequestServer.Controllers;

namespace GameServer.Controllers
{
	public class GameController : Controller
	{
		private readonly ValuesController _valueController;
		private readonly Users _user;
		private readonly HttpClient _http;

		public GameController(ValuesController valueController , HttpClient http)
		{
			_valueController = valueController;

			_http = http;

		}
		[HttpPost]
		public async Task<IActionResult> SendRequest(string token, string userName, HttpClient http)
		{
			try
			{

				if (Users.CheckIfUserOnline(userName))
				{
					if (_valueController.ValidateUser(token, userName))
					{
						bool uerAprove = true;
						ServerLogic.Answer(uerAprove);
						var response = await http.PostAsync("https://localhost:7276", null);// send reqest to game
						return Ok();
					}
					else
					{
						
						return BadRequest("User Validtion failed");
					}
				}
				else
				{
				return BadRequest("User is not online ");
				}

			}
			catch (Exception ex)
			{
				return BadRequest("Cant send reqest " + ex.Message);
			}
		}

		
	}
}


// send game reqest 
// game deneced // cancel or aprove 
//gamestart 
// player move
