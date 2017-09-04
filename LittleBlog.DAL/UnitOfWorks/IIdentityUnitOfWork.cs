using System.Threading.Tasks;
using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.UnitOfWorks;

namespace LittleBlog.DAL.Repositories
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        IAccountManager AccountManager { get; set; }

        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }
    }
}