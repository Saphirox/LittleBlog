using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;

namespace LittleBlog.DAL.Persistence.UnitsOfWork
{
    public class ArticleUnitOfWork : UnitOfWork, IArticleUnitOfWork
    {
        public ArticleUnitOfWork(Context dbContext, IArticleRepository articleRepository, ITagRepository tagRepository) : base(dbContext)
        {
            ArticleRepository = articleRepository;
            TagRepository = tagRepository;
        }
        
        public IArticleRepository ArticleRepository { get; set; }
        public ITagRepository TagRepository { get; set; }
    }
}