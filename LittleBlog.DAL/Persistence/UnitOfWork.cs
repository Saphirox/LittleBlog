using System.Threading.Tasks;
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
        }
    
        public IArticleRepository ArticleRepository { get; set; }
        public ITagRepository TagRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IAccountManager AccountManager { get; set; }

  
        public int Commit()
        {
            return this.DbContext.SaveChanges();
        }
        
        public async Task<int> CommitAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
    }
}