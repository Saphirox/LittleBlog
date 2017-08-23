using System;

namespace LittleBlog.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository ArticleRepository { get; set; }
        ITagRepository TagRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }

        int Commit();
    }
}