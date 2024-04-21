using LoginWpfLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace LoginWpfLogic.Services
{
    public class UserService : IUserManager
    {
        private readonly HttpClient _httpClient;
        private string _token;

        const string url = "api/Auth";

        public UserService() =>
            _httpClient = new HttpClient {
             BaseAddress = new Uri("http://localhost:5146/")
            };

        public async Task<string> LoginAsync(string username,string password)
        {
            var logModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            var respone = await _httpClient.PostAsJsonAsync($"{url}/login", logModel);
            respone.EnsureSuccessStatusCode();


            _token = await respone.Content.ReadAsStringAsync();

            return _token;
        }

        public async Task<HttpResponseMessage> RegisterAsync(string firstname, string lastname, string email, string username, string password, string confirmedpassword)
        {
            var regModel = new RegisterModel
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Username = username,
                Password = password,
                ConfirmedPassword = confirmedpassword
            };

            return await _httpClient.PostAsJsonAsync($"{url}/register", regModel);
        }

        public void AddTokenRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        public string GetUserNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var userNameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (userNameClaim != null)
                {
                    return userNameClaim.Value;
                }
            }

            return null;
        }

        public async Task<IEnumerable<string>> GetAllUsers(string token)
        {
			try
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var response = await _httpClient.GetAsync("http://localhost:5146/api/UsersAuth");

				if (response.IsSuccessStatusCode)
				{
					var usernames = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
					return usernames!;
				}
				else
				{
					Console.WriteLine($"Failed to retrieve users. Status code: {response.StatusCode}");
					return Enumerable.Empty<string>();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving users: {ex.Message}");
				return Enumerable.Empty<string>();
			}
		}


    }
}
