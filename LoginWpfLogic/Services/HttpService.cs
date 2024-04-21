using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LoginWpfLogic.Services
{
	public class HttpService
	{
		private readonly HttpClient _httpClient;
		private readonly string _token;
		public HttpService(HttpClient httpClient, string token)
		{
			_httpClient = httpClient;
			_token = token;
			ConfigureHttpClient();
		}

		public async Task<string> SendRequestAsync(string url)
		{
			try
			{
				var response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException ex)
			{
				return ex.Message;
			}
		}
		private void ConfigureHttpClient()
		{
			if (!string.IsNullOrEmpty(_token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
			}
		}
		public async Task<bool> IsTokenValid(string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				return false;
			}

			try
			{
				// Send a request to a secure endpoint with the token
				HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5146/api/Auth/validate-token",
					token);

				// Check if the response is successful (HTTP status code 200 OK)
				if (response.IsSuccessStatusCode)
				{
					// Token is valid
					return true;
				}
				else
				{
					// Token is invalid
					return false;
				}
			}
			catch (HttpRequestException ex)
			{
				// An error occurred while sending the request
				Console.WriteLine($"Error: {ex.Message}");
				return false;
			}
		}
	}
}