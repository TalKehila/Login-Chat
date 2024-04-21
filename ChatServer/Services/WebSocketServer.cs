using ChatServer.Modal;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
public class WebSocketServer
{
	private readonly ConcurrentDictionary<Guid, WebSocket> _clients;
	private readonly ChatServer.Modal.WebSocketHandler _webSocketHandler;

	public WebSocketServer(ChatServer.Modal.WebSocketHandler webSocketHandler)
	{
		_clients = new ConcurrentDictionary<Guid, WebSocket>();
		_webSocketHandler = webSocketHandler;
	}
	public async Task AcceptWebSocketAsync(HttpContext context)
	{
		if (context.WebSockets.IsWebSocketRequest)
		{
			var webSocket = await context.WebSockets.AcceptWebSocketAsync();
			var clientId = Guid.NewGuid();
			_webSocketHandler.AddClient(clientId, webSocket);
			Console.WriteLine($"Client connected: {clientId}");
			Task.Run(async () => await ReceiveMessagesAsync(clientId, webSocket));
		}
		else
		{
			context.Response.StatusCode = 400;
		}
	}
	public async Task HandleWebSocketAsync(WebSocket webSocket)
	{
		try
		{
			var clientId = Guid.NewGuid();
			_webSocketHandler.AddClient(clientId, webSocket);

			var buffer = new byte[1024];
			while (webSocket.State == WebSocketState.Open)
			{
				var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				if (result.MessageType == WebSocketMessageType.Text)
				{
					var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
					Console.WriteLine($"Received message from {clientId}: {message}");
					await BroadcastMessageAsync(message);
				}
			}
		}
		catch (WebSocketException)
		{
			_webSocketHandler.RemoveClient(webSocket);
		}
	}
	private async Task BroadcastMessageAsync(string message)
	{
		foreach (var client in _webSocketHandler.GetClients())
		{
			if (client.Value.State == WebSocketState.Open)
			{
				await client.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
			}
		}
	}
	public async Task ReceiveMessagesAsync(Guid clientId, WebSocket webSocket)
	{
		try
		{
			var buffer = new byte[1024];
			while (webSocket.State == WebSocketState.Open)
			{
				var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				if (result.MessageType == WebSocketMessageType.Text)
				{
					var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
					Console.WriteLine($"Received message from {clientId}: {message}");
					foreach (var client in _clients)
					{
						if (client.Value.State == WebSocketState.Open)
						{
							await client.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
						}
					}
				}
			}
		}
		catch (WebSocketException)
		{
			_clients.TryRemove(clientId, out _);
			Console.WriteLine($"Client disconnected: {clientId}");
		}
	}
	public void BroadcastMessage(MessageDto message)
	{
		var messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
		foreach (var client in _clients)
		{
			if (client.Value.State == WebSocketState.Open)
			{
				client.Value.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
			}
		}
	}
}
