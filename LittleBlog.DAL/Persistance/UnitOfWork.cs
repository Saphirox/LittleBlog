using LittleBlog.DAL.Repositories;

namespace LittleBlog.DAL.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public Context DbContext { get; set; }

        public UnitOfWork(Context dbContext)
        {
            DbContext = dbContext;
        }
    
        public IArticleRepository ArticleRepository { get; set; }
        public ITagRepository TagRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }

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