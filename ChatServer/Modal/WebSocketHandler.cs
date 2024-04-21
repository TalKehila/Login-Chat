using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace ChatServer.Modal
{
	public class WebSocketHandler
	{
		private readonly ConcurrentDictionary<Guid, WebSocket> _clients;

		public WebSocketHandler()
		{
			_clients = new ConcurrentDictionary<Guid, WebSocket>();
		}

		public void AddClient(Guid clientId, WebSocket webSocket)
		{
			_clients.TryAdd(clientId, webSocket);
		}

		public void RemoveClient(WebSocket webSocket)
		{
			foreach (var client in _clients)
			{
				if (client.Value == webSocket)
				{
					_clients.TryRemove(client.Key, out _);
					break;
				}
			}
		}
		public ConcurrentDictionary<Guid, WebSocket> GetClients()
		{
			return _clients;
		}
		public async Task HandleWebSocketAsync(WebSocket webSocket)
		{
			var buffer = new byte[1024];
			var cancellationToken = CancellationToken.None;

			try
			{
				while (webSocket.State == WebSocketState.Open)
				{

					var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

					if (result.MessageType == WebSocketMessageType.Text)
					{

						var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
						Console.WriteLine("Received message: " + message);

						var responseBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
						await webSocket.SendAsync(responseBuffer, WebSocketMessageType.Text, true, cancellationToken);
					}
					else if (result.MessageType == WebSocketMessageType.Close)
					{
						await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "WebSocket closed", cancellationToken);
					}
				}
			}
			catch (WebSocketException ex)
			{
				Console.WriteLine($"WebSocket Exception: {ex.Message}");
			}
		}
	}
}
