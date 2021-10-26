using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TimerZ.Api.Mapper;
using TimerZ.Common;
using TimerZ.DAL;
using TimerZ.Domain.Models;
using TimerZ.Repository;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services;
using TimerZ.TimerTracking.Services.Interfaces;
using UserProvider = TimerZ.Api.Providers.UserProvider;


namespace TimerZ.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimerZDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                services.AddHttpContextAccessor();
                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TimerZDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, TimerZDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ); 
            //services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddTransient<ILabelsWriteRepository, LabelsRepository>();
            services.AddTransient<ILabelsReadRepository, LabelsRepository>();
            services.AddTransient<IProjectsWriteRepository, ProjectsRepository>();
            services.AddTransient<IProjectsReadRepository, ProjectsRepository>();
            services.AddTransient<ITimerEntryWriteRepository, TimerEntryRepository>();
            services.AddTransient<ITimerEntryReadRepository, TimerEntryRepository>();
            services.AddTransient<ITimeTrackingService, TimeTrackingService>();
            services.AddTransient<ILabelsService, LabelsService>();
            services.AddTransient<IProjectsService, ProjectsService>();

            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ITimerEntryMapper, TimerEntryMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }


            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
