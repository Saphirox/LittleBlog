using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace LittleBlog.DAL.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
            this.PasswordValidator = new PasswordValidator
            {
                RequireDigit = false,
                RequiredLength = 5,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };
        }
    }
}