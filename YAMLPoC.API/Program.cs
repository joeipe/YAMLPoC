using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YAMLPoC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            /*
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.Console()
                //.WriteTo.Console(new JsonFormatter())
                //.WriteTo.Seq("http://localhost:5341")
                //.WriteTo.File("log.txt")
                //.WriteTo.File(new JsonFormatter(), "log.txt")
                //.WriteTo.AzureAnalytics(workspaceId: "04d319c3-6584-4b4f-a0db-05b1e72c92a3", authenticationId: "VcJPWt7kKrC130uFIYo4FGl5g4wKCVTTS2tlUflSH/YARtwAGb0dhX8/1/OcO6ereWdLdLf2QLAeq5fMXrS6zg==", logName: "YAMLPoC")
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            */
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(appConfig =>
                {
                    appConfig.AddJsonFile($"appsettings.json", false, true);
                    appConfig.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
                });
    }
}
