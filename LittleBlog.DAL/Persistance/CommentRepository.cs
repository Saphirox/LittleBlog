using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Persistance
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(Context context) : base(context)
        {
        }


    }
}