using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWpfLogic.DTO
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmedPassword {  get; set; } = string.Empty;
    }
}
