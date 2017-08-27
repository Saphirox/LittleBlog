using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Identity;

namespace LittleBlog.DAL.Persistence
{
    public class AccountManager : IAccountManager
    {
        public Context DbContext { get; set; }

        public AccountManager(Context dbContext)
        {
            DbContext = dbContext;
        }
        
        public void Create(Account account)
        {
            DbContext.Accounts.Add(account);
        }
    }
}