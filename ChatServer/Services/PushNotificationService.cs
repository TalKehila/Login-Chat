//using ChatServer.Modal;
//using FirebaseAdmin.Messaging;

//namespace ChatServer.Services
//{
//	public class PushNotificationService
//	{
//		public void SendPushNotification(MessageDto message)
//		{
//			// Retrieve device token from the MessageDto
//			string recipientDeviceToken = message.Token;

//			if (string.IsNullOrEmpty(recipientDeviceToken))
//			{
//				// Handle case where recipient's device token is not provided
//				return;
//			}

//			// Construct notification message
//			var notification = new Notification
//			{
//				Title = "New Message",
//				Body = $"{message.FromUser}: {message.Message}"
//			};

//			// Construct FCM message
//			var fcmMessage = new Message
//			{
//				Token = recipientDeviceToken,
//				Notification = notification
//			};

//			// Send FCM message
//			FirebaseMessaging.DefaultInstance.SendAsync(fcmMessage);
//		}
//	}
//}
