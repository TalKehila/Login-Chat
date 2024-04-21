using BackGammon_API.DTO;
using BackGammon_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackGammon_API.Service.Interfaces
{
    public interface IUser
    {
        Task<bool> Register(RegisterDTO reg);
        Task<string> Login(LoginDTO log);
        Task<IEnumerable<string>> GetAllUsers();
        Task<ActionResult<User>> GetUserById(string id);
        Task<User>GetUserByUsername(string username);
        
    }
}
