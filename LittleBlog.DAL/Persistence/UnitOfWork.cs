using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public Context DbContext { get; set; }

        public UnitOfWork(
            Context dbContext, 
            IArticleRepository articleRepository,
            ITagRepository tagRepository, 
            ICommentRepository commentRepository)
        {
            DbContext = dbContext;
            ArticleRepository = articleRepository;
            TagRepository = tagRepository;
            CommentRepository = commentRepository;
            
            UserManager =  new AppUserManager(new UserStore<AppUser>(dbContext));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(dbContext));
        }
    
        public IArticleRepository ArticleRepository { get; set; }
        public ITagRepository TagRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }

        public int Commit()
        {
            return this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}