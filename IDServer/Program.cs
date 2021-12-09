using Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IDServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostEnvironment env = null;

            var host = CreateHostBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                env = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                      optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();


            }).ConfigureLogging((hostingContext, logging) => {
                // Requires `using Microsoft.Extensions.Logging;`
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();

                //logging.AddFile();
            })
           .Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                Console.WriteLine(services.GetRequiredService<RestApiContext>().Database.GetConnectionString());
                try
                {
                    var context = services.GetRequiredService<RestApiContext>();
                    Console.WriteLine(context.Database.GetConnectionString());
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
