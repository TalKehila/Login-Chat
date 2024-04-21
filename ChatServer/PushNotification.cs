//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//public class PushNotification
//{
//	private readonly string wnsEndpoint = "https://<your-wns-endpoint>";
//	private readonly string wnsPackageSid = "<your-package-sid>";
//	private readonly string wnsClientSecret = "<your-client-secret>";

//	public PushNotification()
//	{
//		// Constructor logic here
//	}

//	// Method to send push notification to a specific device
//	public async Task SendToDevice(string deviceId, string message)
//	{
//		var payload = $@"<toast><visual><binding template=""ToastText01""><text id=""1"">{message}</text></binding></visual></toast>";

//		var request = new HttpRequestMessage(HttpMethod.Post, wnsEndpoint);
//		request.Headers.Add("X-WNS-Type", "wns/toast");
//		request.Headers.Add("Authorization", $"Bearer {await GetAccessToken()}");
//		request.Content = new StringContent(payload, Encoding.UTF8, "text/xml");

//		using (var client = new HttpClient())
//		{
//			var response = await client.SendAsync(request);
//			response.EnsureSuccessStatusCode();
//		}
//	}

//	// Method to get access token for WNS
//	private async Task<string> GetAccessToken()
//	{
//		var request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/common/oauth2/token");
//		var requestBody = $"grant_type=client_credentials&client_id={Uri.EscapeDataString(wnsPackageSid)}&client_secret={Uri.EscapeDataString(wnsClientSecret)}&scope=notify.windows.com";
//		request.Content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

//		using (var client = new HttpClient())
//		{
//			var response = await client.SendAsync(request);
//			var responseContent = await response.Content.ReadAsStringAsync();
//			dynamic tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
//			return tokenResponse.access_token;
//		}
//	}
//}
