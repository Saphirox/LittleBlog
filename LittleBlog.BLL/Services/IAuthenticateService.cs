using System.Security.Claims;
using System.Threading.Tasks;
using LittleBlog.Dtos.Identity;

namespace LittleBlog.BLL.Services
{
    public interface IAuthenticationService
    {
        Task<ClaimsIdentity> Authenticate(AccountDTO account);
    }
}