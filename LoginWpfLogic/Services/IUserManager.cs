using LoginWpfLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWpfLogic.Services
{
    public interface IUserManager
    {
        Task<string> LoginAsync(string username,string password);

        Task<HttpResponseMessage> RegisterAsync
            (string firstname, string lastname, string email, string username, string password, string confirmedpassword);

        void AddTokenRequestHeaders();

        string GetUserNameFromToken(string token);

		Task<IEnumerable<string>> GetAllUsers(string token);

       
    }
}
