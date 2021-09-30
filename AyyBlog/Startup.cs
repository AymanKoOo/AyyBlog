using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites;
using Core.Interfaces;
using Core.Interfaces.Base;
using Infrastructure.Data;
using Infrastructure.Repoo;
using Infrastructure.Repoo.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AyyBlog
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //  services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("Ayblog")));
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("Ayblog")));



            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.SignIn.RequireConfirmedEmail = true; ;
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
               .AddEntityFrameworkStores<DataContext>();
            //.AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
             options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Auth";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            }
            );

      
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //}).AddCookie(options =>
            //{
            //    options.LoginPath = "/Authentication/Auth";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //    options.SlidingExpiration = true;
            //});



            services.AddAutoMapper(typeof(Startup));
            ///Intailize MVc///
            ///Intalize unitofwork///
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            // services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(IAdminRepo), typeof(AdminRepo));
            services.AddScoped(typeof(IPostRepo), typeof(PostRepo));
            services.AddScoped(typeof(ICategoryRepo), typeof(CategoryRepo));
            services.AddScoped(typeof(ITagRepo), typeof(TagRepo));

            ///Intalize cookie///
            ///
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> _manager,
        RoleManager<ApplicationRole> _roleManager)

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

            // Ensure the following middleware are in the order shown
            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();
            //call seeding//
            MyIdentityDataInitializer ob = new MyIdentityDataInitializer(_manager, _roleManager);
           // ob.SeedData();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "areaRoute",
                      pattern: "{area:exists}/{controller=AdminHome}/{action=Index}/{id?}"

                    );

                endpoints.MapControllerRoute(
                 name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
