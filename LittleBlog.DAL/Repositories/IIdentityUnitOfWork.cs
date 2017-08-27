using System.Threading.Tasks;
using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Persistence;

namespace LittleBlog.DAL.Repositories
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }
    }
}