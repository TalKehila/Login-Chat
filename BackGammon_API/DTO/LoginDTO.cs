using System.ComponentModel.DataAnnotations;

namespace BackGammon_API.DTO
{
    public class LoginDTO
    {
		[Required(ErrorMessage = "User Name Required")]
		public string UserName { get; set; } = "";
		[Required(ErrorMessage = "Password Required")]
		[MinLength(6)]
		public string Password { get; set; } = "";
	}
}
