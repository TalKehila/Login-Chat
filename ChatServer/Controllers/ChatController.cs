using ChatServer.Modal;
using Microsoft.AspNetCore.Mvc;

public class ChatController : ControllerBase
{
	private readonly WebSocketServer _webSocketServer;

	public ChatController(WebSocketServer webSocketServer)
	{
		_webSocketServer = webSocketServer;
	}

	//[Route("api/[controller]")]
	[HttpGet("/ws")]
	//[ApiController]
	public async Task<IActionResult> ConnectToWebSocket()
	{
		if (HttpContext.WebSockets.IsWebSocketRequest)
		{
			var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
			await _webSocketServer.HandleWebSocketAsync(webSocket);
			return NoContent(); 
		}
		else
		{
			return BadRequest("Invalid request");
		}
	}
}





//using ChatServer.Modal;
//using Microsoft.AspNetCore.Mvc;

//namespace ChatServer.Controllers
//{
//	//[Route("/ws")]
//	[Route("api/[controller]")]
//	[ApiController]
//	public class ChatController : ControllerBase
//	{
//		private readonly WebSocketServer _webSocketServer;

//		public ChatController(WebSocketServer webSocketServer)
//		{
//			_webSocketServer = webSocketServer;
//		}

//		//[Route("/ws")]
//		[HttpPost("sendMessage")]
//		public IActionResult SendMessage([FromBody] MessageDto message)
//		{
//			_webSocketServer.BroadcastMessage(message);
//			return Ok();
//		}
//	}
//}













//using ChatServer.Modal;
//using ChatServer.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace ChatServer.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class ChatController : ControllerBase
//	{
//		private readonly ChatService _chatService;
//		private readonly PushNotificationService _pushNotificationService;

//		public ChatController(ChatService chatService, PushNotificationService pushNotificationService)
//		{
//			_chatService = chatService;
//			_pushNotificationService = pushNotificationService;
//		}

//		[HttpPost("sendMessage")]
//		public IActionResult SendMessage([FromBody] MessageDto message)
//		{
//			_chatService.SendMessage(message);
//			_pushNotificationService.SendPushNotification(message);
//			return Ok();
//		}
//	}
//}