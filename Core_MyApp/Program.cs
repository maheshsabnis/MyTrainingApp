using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core_MyApp
{
    public class Program
    {
        /// <summary>
        /// Entry Point to ASP.NETb Core App
        /// Hosting Process Invokes Main()
        /// and start execution. Creates an instance of HostBuilder(?) clas
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Configure the Hosting Process with required dependencies
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // create hosting env. with default settings
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Invoke the Startup class to initialize the 
                    // default hosting
                    // webBuilder.UseStartup<Startup>(); will inject 
                    // the 'IConfiguration' interface in Startup class and
                    // instantiate the Startup class
                    // Startup class will initialize the application with
                    // <HttpApplication Block Equivalent> 
                    //     Depednencies, WebForm/MVC/API request processing, sessions, caching, identity, etc
                    //  <HttpModule Equivaent>
                    //      Initialize Middlewares to manage request Processing 
                    //
                    webBuilder.UseStartup<Startup>();
                });
    }
}
