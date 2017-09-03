using System.Security.Principal;
using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistence
{
    public class IdentityUnitOfWork : UnitOfWork, IIdentityUnitOfWork
    {
        public IdentityUnitOfWork(
            Context dbContext,
            IArticleRepository articleRepository,
            ITagRepository tagRepository,
            ICommentRepository commentRepository
        )
            : base(dbContext, articleRepository, tagRepository, commentRepository)
        {
            UserManager =  new AppUserManager(new UserStore<AppUser>(dbContext));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(dbContext));
        }
        
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }
    }
}