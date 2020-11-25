using Ninject.Modules;
using Project.Service.Common;
using Project.Service.Twitch;

namespace Project.Service
{
    public class ServiceDIModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUserService>().To<UserService>();
            Kernel.Bind<ICompanyService>().To<CompanyService>();
            Kernel.Bind<ITwitchAuthenticationService>().To<TwitchAuthenticationService>();
        }
    }
}