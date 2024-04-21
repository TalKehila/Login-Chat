using BackGammon_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackGammon_API.Data
{
    public class ApplicationData : IdentityDbContext<User>
    {
        public ApplicationData(DbContextOptions<ApplicationData> options): base(options) { }
    }
}
