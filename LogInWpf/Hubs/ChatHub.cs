//using Microsoft.AspNetCore.SignalR.Client;
//using System;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;


//namespace LogInWpf.Hubs
//{
//    public class ChatHub
//    {
//        private readonly HubConnection _connection;

//        public event Action<string, string>? MessageReceived;

//        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

//        public ChatHub()
//        {
//            _connection = new HubConnectionBuilder()
//                .WithUrl("http://localhost:5146/chathub")
//                .Build();

//            _connection.On<string, string>("ReceiveMessage", (user, message) =>
//            {
//                MessageReceived?.Invoke(user, message);
//            });
//        }

//        public async Task StartConnection()
//        {
//            try
//            {
//                await _connection.StartAsync();
//                Console.WriteLine("SignalR connection started.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error starting SignalR connection: {ex.Message}");
//            }
//        }

//        public async Task SendMessage(string sender, string recipient, string message)
//        {
//            try
//            {
//                await _connection.InvokeAsync("SendMessage", sender, recipient, message);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error sending message via SignalR: {ex.Message}");
//            }
//        }
//    }
//}
