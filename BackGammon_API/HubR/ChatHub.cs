using BackGammon_API.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BackGammon_API.HubR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Broadcast the message to all clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            // Implement additional logic when a client connects
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Implement additional logic when a client disconnects
            await base.OnDisconnectedAsync(exception);
        }
    }
}
