//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementApp.Data;
using ManagementApp.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using AppContext = ManagementApp.Models.AppContext;
using System;

namespace ManagementApp {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
                {
                var services = scope.ServiceProvider;
                try
                    {
                    var context = services.GetRequiredService<Models.AppContext>();
                    Dbinitializer.Inittialize(context);
                    }
                catch (Exception ex)
                    {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }

            host.Run();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
