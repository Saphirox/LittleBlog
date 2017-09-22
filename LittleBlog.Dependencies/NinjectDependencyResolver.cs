using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.BLL.Services;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Mapper;
using Ninject;
using Ninject.Web.Common;
using LittleBlog.DAL.Persistence.Repositories;

namespace LittleBlog.Dependencies
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /* Services */
            _kernel.Bind<IArticleService>().To<ArticleService>().InRequestScope();
            _kernel.Bind<IViewArticleService>().To<ViewArticleService>().InRequestScope();
            _kernel.Bind<IFileService>().To<FileService>().InRequestScope();
            _kernel.Bind<ICommentService>().To<CommentService>().InRequestScope();
            _kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            _kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();
            _kernel.Bind<ILoggerService>().To<LoggerService>().InRequestScope();

            /* Repositories */
            _kernel.Bind<IArticleRepository>().To<ArticleRepository>().InRequestScope();
            _kernel.Bind<ITagRepository>().To<TagRepository>().InRequestScope();
            _kernel.Bind<ILoggerRepository>().To<LoggerRepository>().InRequestScope();

            /* Unit of Works*/
            _kernel.Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().InRequestScope();
            _kernel.Bind<ILoggerUnitOfWork>().To<LoggerUnitOfWork>().InRequestScope();
            _kernel.Bind<IArticleUnitOfWork>().To<ArticleUnitOfWork>().InRequestScope();

            /* Managers */
            _kernel.Bind<IAccountManager>().To<AccountManager>().InRequestScope();
            
            /* Shared */
            _kernel.Bind<Context>().ToSelf().InSingletonScope();
            _kernel.Bind<IMapper>().ToConstant(MapperBuilder.BuildMapper()).InSingletonScope();

        }
    }
}