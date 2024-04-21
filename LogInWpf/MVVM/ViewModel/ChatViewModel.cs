using GalaSoft.MvvmLight.Command;
using LogInWpf.Core;
using LogInWpf.MVVM.Model;
using LoginWpfLogic.Services;
using Microsoft.AspNetCore.Http;
using Polly.Caching;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Windows;

namespace LogInWpf.MVVM.ViewModel
{
	public class ChatViewModel : ObservableObject
	{
		private readonly IUserManager _userService = new LoginWpfLogic.Services.UserService();
		private readonly IHttpContextAccessor _httpContextAccessor;
		public string NowUser { get; set; }
		private ClientWebSocket _webSocket;
		public string CurrentUserName { get; private set; }

		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		private ObservableCollection<UserModel> _users;
		public ObservableCollection<UserModel> Users
		{
			get { return _users; }
			set { SetProperty(ref _users, value); }
		}

		private UserModel _selectedUser;
		public UserModel SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				SetProperty(ref _selectedUser, value);
				SendMessageCommand.RaiseCanExecuteChanged(); // Update command can execute
			}
		}

		public GalaSoft.MvvmLight.Command.RelayCommand SendMessageCommand { get; }
		public RelayCommand<UserModel> SelectUserCommand { get; }

		public ChatViewModel()
		{
			
			CurrentUserName = GetCurrentUserName();
			SendMessageCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () => await SendMessage(), () => CanSendMessage());
			SelectUserCommand = new RelayCommand<UserModel>(SelectUser);
			_webSocket = new ClientWebSocket();
			LoadUsers();

			//ConnectToServer();
		}

		private string GetCurrentUserName()
		{
			try
			{
				if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
				{
					return "Unknown"; // Default value
				}

				// Retrieve the JWT token from the Authorization header
				string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);

				// Decode the token to get the claims
				var handler = new JwtSecurityTokenHandler();
				var decodedToken = handler.ReadJwtToken(token);

				// Extract the username claim
				var userNameClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

				// Return the username
				return userNameClaim?.Value ?? "Unknown";
			}
			catch (Exception ex)
			{
				// Handle any exceptions (e.g., token parsing errors)
				// Log the exception or return a default value
				return "Unknown"; // Default value
			}
		}


		public string msgs = "";
		int ttt = 0;
		bool isConnected = false;


		private async void ConnectToServer(string mmm)
		{
			msgs = "";
			// Create a TcpClient
			TcpClient client = new TcpClient("127.0.0.1", 8888);
			NetworkStream stream = null;
			// Get a client stream for reading and writing
			stream = client.GetStream();

			// Set the server IP address and port number
			try
			{

				// Buffer for reading data
				byte[] bytes = new byte[1024];

				// Send initial message to the server
				string data = CurrentUserName + ";srv;online;";
				byte[] msg = Encoding.ASCII.GetBytes(data);
				stream.Write(msg, 0, msg.Length);

				while (true)
				{
					while ((msgs == "") && (stream.DataAvailable == false))
					{
						await Task.Delay(1000);
					}

					// This part sends data - Don't touch!!!!
					if (msgs != "")
					{
						// Prepare the message to be transmitted to the server
						string dataToSend = CurrentUserName + ";" + SelectedUser + ";msg;" + Message;
						byte[] msgToSend = Encoding.ASCII.GetBytes(dataToSend);

						// Send the message to the connected server
						stream.Write(msgToSend, 0, msgToSend.Length);
						Console.WriteLine("Sent: {0}", dataToSend);
						msgs = "";
					}
					else
					{
						// This part receives data - Add all login

						// Receive the response from the server
						int bytesRead = stream.Read(bytes, 0, bytes.Length);
						string responseData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
						Console.WriteLine("Received: {0}", responseData);

						string[] sss = responseData.Split(';');

						if (sss[0] == "srv" && sss[2] == "online")
						{
							// update UI online user list
						}

						if (sss[2] == "msg")
						{
							// find user sss[0] (from)
							// add UI message sss[3] 
						}
					}
				}

				stream.Close();
				client.Close();
			}
			catch (ArgumentNullException e)
			{
				isConnected = false;
				Console.WriteLine("ArgumentNullException: {0}", e);
			}
			catch (SocketException e)
			{
				isConnected = false;
				Console.WriteLine("SocketException: {0}", e);
			}

			isConnected = false;

			Console.WriteLine("\nPress Enter to exit...");
			Console.ReadLine();
		}


		private async Task LoadUsers()
		{
			var token = TokenManager.Token;
			try
			{
				var userService = new UserService();
				var currentUser = userService.GetUserNameFromToken(token);
				IEnumerable<string> usernames = await _userService.GetAllUsers(token);
				usernames = usernames.Where(username => username != currentUser);

				Users = new ObservableCollection<UserModel>(
					usernames.Select(username => new UserModel { UserName = username })
				);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading users: {ex.Message}");
			}
		}
		private async void ListenForMessages()
		{
			var buffer = new byte[1024];
			while (_webSocket.State == WebSocketState.Open)
			{
				var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				if (result.MessageType == WebSocketMessageType.Text)
				{
					var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
					// Update UI with received message if needed
				}
			}
		}
		private async Task SendMessage()
		{
			if (isConnected == false)
			{
				ConnectToServer("aaa");
				isConnected = true;
			}
			ttt++;
			// TODOOOOOOOOOOOOOOOOOOOO
			//msgs = ttt.ToString();
			//msgs = CurrentUserName+";"+SelectedUser+";msg;"+text;
			msgs = CurrentUserName + ";" + SelectedUser + ";msg;" + Message;


			try
			{
				if (SelectedUser != null && !string.IsNullOrWhiteSpace(Message))
				{
					var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(Message));
					await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
					Message = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool CanSendMessage()
		{
			return true;
			//return SelectedUser != null && Users.Contains(SelectedUser) && !string.IsNullOrWhiteSpace(Message);
		}
		private void SelectUser(UserModel user)
		{
			SelectedUser = user;
		}
	}
}
