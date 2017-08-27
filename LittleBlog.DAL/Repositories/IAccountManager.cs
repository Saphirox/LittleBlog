using System.Security.Claims;
using System.Threading.Tasks;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Identity;

namespace LittleBlog.DAL.Repositories
{
    public interface IAccountManager
    {
        void Create(Account account);
    }
}