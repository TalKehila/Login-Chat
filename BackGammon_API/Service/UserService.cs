using BackGammon_API.DTO;
using BackGammon_API.Helpers;
using BackGammon_API.Models;
using BackGammon_API.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackGammon_API.Service
{
    public class UserService : IUser
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        


        public UserService(UserManager<User> userManager,JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }
        public async Task<string> Login(LoginDTO log)
        {
            var user = await _userManager.FindByNameAsync(log.UserName);
            if(user != null  && await _userManager.CheckPasswordAsync(user, log.Password))
            {
                var token = _jwtHandler.GenerateJwtToken(user);
                return token;
            }
            else
            {
                return "Invalid Login Action";
            }
        }

        public async Task<bool> Register(RegisterDTO reg)
        {
            var user = new User
            {
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                UserName = reg.UserName,
                Email = reg.Email,
            };
            var result = await _userManager.CreateAsync(user,reg.Password);
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var usernames = users.Select(u => u.UserName);
            return usernames!;
        }
		public List<KeyValuePair<string, string>> GetOnlineUsers()
		{
            RequestServer.Controllers.ValuesController vc = new RequestServer.Controllers.ValuesController();
			return vc.GetOnlineUsers();
		}

		public async Task<ActionResult<User>> GetUserById(string id)
        {
             var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.Id == id);
            return user!;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.UserName == username);
            return user!;
        }
        


    }
}
