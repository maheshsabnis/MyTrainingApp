using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Core_MyApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core_MyApp.Models;
using Core_MyApp.Repositories;
using Core_MyApp.CustomFilters;

namespace Core_MyApp
{
    public class Startup
    {
        /// <summary>
        /// IConfiguration, used to read AppSettings from appsettings.json 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// This method will e invoked by hosting immediately after the Constructor
        /// IServiceCollection, will be used to provide an access of DI Container to the
        /// Current application, so that the requierd dependencies will be injected.
        /// Folowing services can be registered in DI
        ///     1. DbContext for Data Access
        ///     2. Sessions
        ///     3. Caching
        ///     4. Request Processing
        ///     5. Custom Repositories
        ///     6. Idenitty
        ///     7....and many more
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // register the COmpanyContext class in Dependency
            // read the connection string from appSettings from "ConnectionStrings" section
            // the AddDbContext() method will instantiate the DbContext class for a Scope (Stateful) 
            services.AddDbContext<CompanyContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString"));
            });

            //Register all Repository classes
            services.AddScoped<IRepository<Department, int>, DepartmentRepository>();
            services.AddScoped<IRepository<Employee, int>, EmployeeRepository>();



            // the method for request processing of MVC and API Controllers
            // define the Action Filters at Global Level
            services.AddControllersWithViews(options => {
                options.Filters.Add(new MyLoggingFilter());
                // resister the custom exception filter
                // the 
                options.Filters.Add(typeof(MyExceptionFilterAttribute));
            });
            // the method for request procesing of WebForms Rezor pages
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// The method used for Executing the Request Processing using 
        /// HttpContext class, the class used for managing Request/Responses.
        /// This method uses middlewares to process request
        /// This method uses middlwares which is a conceptual replacement of HttpModule and HttpHandler
        /// IApplicationBuilder, to build and register Middlewares
        /// IWebHostEnvironment, to uses Host settings to execute application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            // Middleware used to read all  static files from server from the 'wwwroot' folder
            // and will include these files in the HttpResponse of teh View
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // The Application will be exposed on the endpoint for MVC Routing
            // using MapControllerRoute() method will use AddControllersWithViews() from Services
            // for Razor Web Forms using MapRazorPages(), uses AddRazorPages() from serviices
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
