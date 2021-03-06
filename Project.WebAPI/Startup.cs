﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Project.DAL.Context;
using Project.MVC.Ninject;
using Project.Repository;
using Project.Service;
using Project.Service.Common;

namespace Project.WebAPI
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => 
            Kernel.Get(type);
        private object RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //ninject 

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope(), Configuration.GetConnectionString("SQLserverConnection"));
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);

            services.AddCors(options => options.AddPolicy("policy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            this.Kernel = this.RegisterApplicationComponents(app);


            app.UseCors("policy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }



        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();

            var settings = new NinjectSettings();

            settings.LoadExtensions = true;
            settings.ExtensionSearchPatterns =
            settings.ExtensionSearchPatterns.Union(new string[]
            {"*.dll"}).ToArray();


            foreach (var item in settings.ExtensionSearchPatterns)
            {
                Debug.WriteLine("something: " + item);
            }

            var kernel = new StandardKernel(settings);


            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }

            // This is where our bindings are configurated
            kernel.Bind<VehicleContext>().ToSelf().InScope(RequestScope).WithConstructorArgument("options", new DbContextOptionsBuilder<VehicleContext>().UseSqlServer(Configuration.GetConnectionString("SQLserverConnection")).Options);

            //// Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }

    }
}
