using System.Linq;
using AutoMapper;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;

namespace LittleBlog.BLL.Services.Implementation
{
    public class CommentService : Service<IArticleUnitOfWork>, ICommentService
    {
        public CommentService(IArticleUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        
        public void CreateCommentByArticleId(CommentDTO comment, int id)
        {
            var article = UnitOfWork.ArticleRepository.GetById(id);
            article.Comments.Add(Mapper.Map<CommentDTO, Comment>(comment));
            UnitOfWork.Commit();
        }

        public void DeleteCommentByArticleId(int commentId, int articleId)
        {
            var article = UnitOfWork.ArticleRepository.GetById(articleId);
            article.Comments.Remove(article.Comments.SingleOrDefault(a => a.Id == commentId));
            UnitOfWork.Commit();
        }
    }
}