using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Web.Mvc;
using ArbitraryTasks.Storage;

namespace Management_arbitrary_tasks.Infrastructure
{
    public class NinjectControllerFactory : IDependencyResolver
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AdditionOfBindings();
        }

        public object GetService(Type serviceType)
        {
            return ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ninjectKernel.GetAll(serviceType);
        }

        private void AdditionOfBindings()
        {
            ninjectKernel.Bind<IStorageData>().To<StorageData>().WithConstructorArgument("configurationString", "Arbitrary_tasks");
        }

    }
}
