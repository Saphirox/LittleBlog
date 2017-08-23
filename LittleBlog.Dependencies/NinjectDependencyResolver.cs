using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace LittleBlog.Dependencies
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel kernel;



        public object GetService(Type serviceType)
        {
            NinjectResolver
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}