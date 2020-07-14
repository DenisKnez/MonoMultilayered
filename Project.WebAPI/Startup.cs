using FluentMigrator.Runner;
using IdentityServer4;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Project.DAL;
using Project.DAL.Context;
using Project.MVC.Ninject;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Project.WebAPI
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);

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
            //identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryClients(Config.Clients)
                //.AddAspNetIdentity<ApplicationUser>()
                .AddApiAuthorization<ApplicationUser, DatabaseContext>();

            services.AddAuthentication(IdentityServerConstants.DefaultCookieAuthenticationScheme).AddIdentityServerJwt();

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //    // register your IdentityServer with Google at https://console.developers.google.com
            //    // enable the Google+ API
            //    // set the redirect URI to https://localhost:5001/signin-google
            //    options.ClientId = "copy client ID from Google here";
            //    options.ClientSecret = "copy client secret from Google here";

            //});

            //ninject
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope(), Configuration.GetConnectionString("PostgreDatabase"));
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);

            services.AddControllers().AddNewtonsoftJson();
            services.AddFluentMigratorCore();
            services.ConfigureRunner(rb =>
                rb.AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("PostgreDatabase"))
                    .ScanIn(Assembly.Load(Assembly.GetAssembly(typeof(DatabaseContext)).GetName()))
                    .For.Migrations()
                    .For.EmbeddedResources());

            services.AddLogging(lb => lb.AddFluentMigratorConsole());

            //services.AddCors(options => options.AddPolicy("policy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseIdentityServer();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();

            var settings = new NinjectSettings();

            settings.LoadExtensions = true;
            settings.ExtensionSearchPatterns =
            settings.ExtensionSearchPatterns.Union(new string[]
            {"*.dll"}).ToArray();

            var kernel = new StandardKernel(settings);

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }

            // This is where our bindings are configurated
            kernel.Bind<DatabaseContext>().ToSelf().InScope(RequestScope).WithConstructorArgument("options", new DbContextOptionsBuilder<DatabaseContext>().UseNpgsql(Configuration.GetConnectionString("PostgreDatabase")).Options);

            //// Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IOptions<OperationalStoreOptions>>);
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);
            kernel.BindToMethod(app.GetRequestService<IServiceProvider>);
            kernel.BindToMethod(app.GetRequestService<IConfiguration>);

            return kernel;
        }
    }
}