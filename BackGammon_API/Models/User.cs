using Microsoft.AspNetCore.Identity;

namespace BackGammon_API.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
        
    }
	
}
