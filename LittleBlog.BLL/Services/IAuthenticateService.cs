using System.Security.Claims;
using System.Threading.Tasks;
using LittleBlog.Dtos.Identity;

namespace LittleBlog.BLL.Services
{
    public interface IAuthenticateService
    {
        Task<ClaimsIdentity> Authenticate(AccountDTO account);
    }
}