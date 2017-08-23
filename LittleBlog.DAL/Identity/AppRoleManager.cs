using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace LittleBlog.DAL.Identity
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {
        }
    }
}