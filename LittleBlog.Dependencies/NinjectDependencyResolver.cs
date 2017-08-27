using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.BLL.Services;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using LittleBlog.Mapper;
using Ninject;
using Ninject.Web.Common;

namespace LittleBlog.Dependencies
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /* Services */
            kernel.Bind<IArticleService>().To<ArticleService>().InRequestScope();
            kernel.Bind<ICommentService>().To<CommentService>().InRequestScope();

            /* Repositories */
            kernel.Bind<IArticleRepository>().To<ArticleRepository>().InRequestScope();
            kernel.Bind<ICommentRepository>().To<CommentRepository>().InRequestScope();
            kernel.Bind<ITagRepository>().To<TagRepository>().InRequestScope();
            
            /* Unit of Works*/
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().InRequestScope();

            /* Managers */
            kernel.Bind<IAccountManager>().To<AccountManager>().InRequestScope();
            kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            
            /* Shared */
            kernel.Bind<Context>().ToSelf().InRequestScope();
            kernel.Bind<IMapper>().ToConstant(MapperBuilder.BuildMapper()).InSingletonScope();
        }
    }
}