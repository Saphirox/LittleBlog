using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Dtos.Identity;

namespace LittleBlog.BLL.Services
{
    public interface IAccountService
    {
        void Create(AccountDTO accountDto);
        
        AccountDTO GetUserByName(string userName);
    }
}
