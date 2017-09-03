using System;
using System.Threading.Tasks;
using LittleBlog.DAL.Identity;

namespace LittleBlog.DAL.Repositories
{
    public interface IUnitOfWork
    {
        IArticleRepository ArticleRepository { get; set; }
        ITagRepository TagRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IAccountManager  AccountManager { get; set; }

        
        int Commit();
        Task<int> CommitAsync();
    }
}