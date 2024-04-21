using ChatServer.Modal;
using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("localhost:7224");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebSocketHandler>();

var app = builder.Build();
var ws = new ClientWebSocket();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseWebSockets();
app.Map("/ws", async context =>
{
	if (context.WebSockets.IsWebSocketRequest)
	{
		using var ws = await context.WebSockets.AcceptWebSocketAsync();
		var message = "Hello, world!";
		var bytes = Encoding.UTF8.GetBytes(message);
		var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

		while (true)
		{
			if (ws.State == WebSocketState.Open)
			{
				await ws.SendAsync(arraySegment,
					WebSocketMessageType.Text,
					true,
					CancellationToken.None);
			}
			else if (ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
			{
				break;
			}
			Thread.Sleep(1000);
		}
	}
	else
	{
		context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
	}
});


await app.RunAsync();

var webSocketOptions = new WebSocketOptions
{
	KeepAliveInterval = TimeSpan.FromMinutes(2)
};
webSocketOptions.AllowedOrigins.Add("https://localhost:7224");
app.UseWebSockets(webSocketOptions);

app.Use(async (context, next) =>
{
	if (context.Request.Path == "/ws")
	{
		if (context.WebSockets.IsWebSocketRequest)
		{
			var webSocket = await context.WebSockets.AcceptWebSocketAsync();
			var webSocketHandler = app.Services.GetRequiredService<WebSocketHandler>();
			await webSocketHandler.HandleWebSocketAsync(webSocket);
			return;
		}
		else
		{
			context.Response.StatusCode = 400;
		}
	}
	else
	{
		await next();
	}
});
app.MapControllers();
app.Run();

//var receiveTask = Task.Run(async () =>
//{
//var buffer = new byte[1024];
//	while (true)
//	{

//		var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer),
//		CancellationToken.None);
//		if (result.MessageType == WebSocketMessageType.Close)
//		{
//			break;
//		}
//		var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//		await Console.Out.WriteLineAsync("Recieved :" + message);

//	}
//});

//await receiveTask;