using System.ComponentModel.DataAnnotations;

namespace BackGammon_API.DTO
{
    public class RegisterDTO
    {
		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = "";

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = "";

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; } = "";

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = "";

		[Required(ErrorMessage = "Confirmed password is required")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmedPassword { get; set; } = "";

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = "";
	}
}
