using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using Ninject.Modules;

namespace LittleBlog.Dependencies.DLL
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IArticleRepository>().To<ArticleRepository>();
            this.Bind<ITagRepository>().To<TagRepository>();
            this.Bind<ICommentRepository>().To<CommentRepository>();
        }
    }
}