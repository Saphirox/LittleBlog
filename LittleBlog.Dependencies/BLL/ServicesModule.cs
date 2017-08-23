using LittleBlog.BLL.Services;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.DAL.Repositories;
using Ninject.Modules;

namespace LittleBlog.Dependencies.BLL
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IArticleService>().To<ArticleService>();
        }
    }
}