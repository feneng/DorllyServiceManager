using System;
using DorllyService.Domain;
using DorllyService.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DorllyServiceManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //管理员用户的cookie服务
                .AddCookie(AdminAuthorizeAttribute.AdminAuthenticationScheme, options => {
                    options.LoginPath = "/Admin/Login/Index";
                    options.LogoutPath = "";
                    options.AccessDeniedPath = "";
                    options.Cookie.Path = "/";
                })
                //普通用户的cookie服务
                .AddCookie(UserAuthorizeAttribute.UserAuthenticationScheme, options =>
                {
                    options.LoginPath = "";
                    options.LogoutPath = "";
                    options.AccessDeniedPath = "";
                    options.Cookie.Path = "/";
                });

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IModuleManager, ModuleManager>();
            services.AddTransient<IPermissionManager, PermissionManager>();
            services.AddTransient<IRoleManager, RoleManager>();

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<DorllyServiceManagerContext>(options =>
                    options.UseLoggerFactory(DorllyServiceManagerContext.LogFactory)
                    .UseSqlite(Configuration.GetConnectionString("TheOtherSqlServiceConnection"),
                    b=>b.MigrationsAssembly("DorllyServiceManager")));
            }
            else
            {
                services.AddDbContext<DorllyServiceManagerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServiceConnection"),
                    b => b.MigrationsAssembly("DorllyServiceManager")));
            }
            
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "areaRoute",
                    areaName:"Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
