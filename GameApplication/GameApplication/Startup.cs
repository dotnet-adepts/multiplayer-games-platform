using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Controllers;
using GameApplication.Services.GamesSessions;
using GameApplication.Data;
using GameApplication.Repositories;
using GameApplication.Repositories.Interfaces;
using GameApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameApplication.Data;
using GameApplication.Models;
using GameApplication.Services;

namespace GameApplication
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=linux_dev.db"));
            // TODO revert changes
            // MS SqlServer throws NotSupported on Linux platform, so I temporarily changed it to Sqlite
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:client_id"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:client_secret"];
            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddSignalR();

            services.AddSingleton<GameService>();
            services.AddSingleton<LobbyService>();
            services.AddSingleton<BattleshipSessionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Games}/{action=Index}/{id?}");
                
                routes.MapRoute(
                    name: "GeneralChat",
                    template: "{controller=GeneralChat}/{action=Index}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatRoom>("general_chat");
            });
        }
    }
}
