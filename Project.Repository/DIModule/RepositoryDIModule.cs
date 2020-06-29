using Ninject.Modules;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository
{
    public class RepositoryDIModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            Kernel.Bind<IUserRepository>().To<UserRepository>();
            Kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
        }
    }
}
