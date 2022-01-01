using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using _2021_ZubakovSemon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using _2021_ZubakovSemon.Controllers;
using Wkhtmltopdf.NetCore;

namespace _2021_ZubakovSemon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(300);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<ILiteDbContext,LiteDbContext>(options => new LiteDbContext(Configuration.GetConnectionString("DatabaseLocation")));

            services.AddTransient<IPasswordValidator<User>,
            CustomPasswordValidator>(serv => new CustomPasswordValidator(6));

            services.AddTransient<IUserValidator<User>, CustomUserValidator>();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();
           
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Policy", policy => policy.RequireRole("Admin"));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseFastReport();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();
            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
