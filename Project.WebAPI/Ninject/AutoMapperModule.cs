using AutoMapper;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Ninject
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IValueResolver<SourceEntity, DestModel, bool>>().To<MyResolver>();

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                List<Profile> profiles = new List<Profile>();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.Contains("Project.")))
                {
                    var notProfiles = assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));
                    foreach (var profile in notProfiles)
                    {
                        Debug.WriteLine($"Profile: {profile.Name}, Assembly: {profile.Assembly}");
                    }

                    profiles.AddRange(assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => (Profile)Activator.CreateInstance(t)));
                }

                // Add all profiles in current assembly
                cfg.AddProfiles(profiles);
            });

            return config;
        }
    }
}
