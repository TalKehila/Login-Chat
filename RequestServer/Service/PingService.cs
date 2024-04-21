//using FirebaseAdmin;
//using FirebaseAdmin.Messaging;
//using Google.Apis.Auth.OAuth2;
//using LoginWpfLogic.Services;
//using System;
//using System.Net.NetworkInformation;
//using System.Threading.Tasks;

//namespace RequestServer.Service
//{
//	public class PingService
//	{
//		private readonly UserService _userService;

//		public PingService(UserService userService)
//		{
//			_userService = userService;
//		}

//		private async Task SendPushNotification(string username, string message)
//		{
//			var firebaseApp = FirebaseApp.Create(new AppOptions()
//			{
//				Credential = GoogleCredential.FromAccessToken("")
//			});

//			var messaging = FirebaseMessaging.DefaultInstance;

//			string deviceToken = await _userService.GetUserDeviceToken(username); //maybe add an method in API server Acceess login 

//			if (deviceToken == null)
//			{
//				//case there is not token 
//				Console.WriteLine($"User {username} does not have a device token registered.");
//				return;
//			}

//			var messageBody = new Message
//			{
//				Notification = new Notification
//				{
//					Title = "Ping Notification",
//					Body = message
//				},
//				Token = deviceToken
//			};

//			await messaging.SendAsync(messageBody);
//		}

//		public async Task SendPing(string username, string token, string address, int timeout)
//		{
//			if (_userService != null)
//			{
//				if (!_userService.ValidateUser(username, token)) // user service APi method or 
//				{
//					return;
//				}


//				using (var ping = new Ping())
//				{
//					try
//					{
//						var pingReply = await ping.SendPingAsync(address, timeout);
//						if (pingReply.Status == IPStatus.Success)
//						{
//							await SendPushNotification(username, "Device is online!");
//						}
//						else
//						{
//							Console.WriteLine($"Ping failed for {address}: {pingReply.Status}");
//						}
//					}
//					catch (Exception ex)
//					{
//						Console.WriteLine($"Error sending ping: {ex.Message}");
//					}
//				}
//			}
//		}
//	}
//}
