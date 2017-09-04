using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistence.UnitsOfWork
{
    public class IdentityUnitOfWork : UnitOfWork, IIdentityUnitOfWork
    {
        public IdentityUnitOfWork(
            Context dbContext, 
            IAccountManager manager) : base(dbContext)
        {
            AccountManager = manager;
            UserManager = new AppUserManager(new UserStore<AppUser>(dbContext));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(dbContext));
        }

        public IAccountManager AccountManager { get; set; }
        
        public AppUserManager UserManager { get; }
       
        public AppRoleManager RoleManager { get; }
    }
}