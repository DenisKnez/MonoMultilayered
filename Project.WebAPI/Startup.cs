using AutoWrapper;
using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Project.DAL.Context;
using Project.MVC.Ninject;
using Project.Service.Twitch;
using Project.WebAPI.FluentValidation.User;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Project.WebAPI
{
    public class Startup
    {
        private readonly string CorsName = "ReactFrontEnd";

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
            string scope = "channel:read:subscriptions";

            // TWITCH API AUTH
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "api/TwitchAuthentication/twitch-login";
                })
                .AddTwitch("twitch", options =>
                {
                    options.ClientId = Configuration.GetSection("TwitchAuth")["ClientId"];
                    options.ClientSecret = Configuration.GetSection("TwitchAuth")["ClientSecret"];
                    options.AuthorizationEndpoint = $"https://id.twitch.tv/oauth2/authorize?response_type=code&scope=${scope}";
                });

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsName, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                });
            });

            //ninject
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope(), Configuration.GetConnectionString("PostgreDatabase"));
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);

            // add newtonsoftjson to the endpoint
            services.AddControllers().AddNewtonsoftJson();

            // migrations
            services.AddFluentMigratorCore();

            // configuraction for the migration runner
            services.ConfigureRunner(rb =>
                rb.AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("PostgreDatabase"))
                    .ScanIn(Assembly.Load(Assembly.GetAssembly(typeof(DatabaseContext)).GetName()))
                    .For.Migrations()
                    .For.EmbeddedResources());

            // logging for the fluent migrator
            services.AddLogging(lb => lb.AddFluentMigratorConsole());
            services.AddTransient(s => s.GetService<HttpContext>().User);
            // http

            services.AddHttpClient<ITwitchAuthenticationService, TwitchAuthenticationService>(options =>
            {
            });

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>()).SetCompatibilityVersion(CompatibilityVersion.Latest);
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
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IsDebug = true,
                UseCustomSchema = true
            }); ;

            app.UseCors(CorsName);

            this.Kernel = this.RegisterApplicationComponents(app);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Setting up ninject DI
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
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
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);
            kernel.BindToMethod(app.GetRequestService<IServiceProvider>);
            kernel.BindToMethod(app.GetRequestService<IConfiguration>);

            return kernel;
        }
    }
}