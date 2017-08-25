using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface ICommentService
    {
        void CreateCommentByArticleId(CommentDTO comment, int articleId);

        void DeleteCommentByArticleId(int commentId,int articleId);
    }
}