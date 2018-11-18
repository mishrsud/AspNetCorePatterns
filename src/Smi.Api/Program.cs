using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace Smi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(ConfigureSerilog);

        private static void ConfigureSerilog(
            WebHostBuilderContext webHostBuilderContext, 
            ILoggingBuilder loggingBuilder)
        {
            Log.Logger = CreateLoggerConfiguration(
                            webHostBuilderContext.Configuration, 
                            webHostBuilderContext.HostingEnvironment.IsDevelopment())
                        .CreateLogger();

            loggingBuilder.AddSerilog();
        }

        /// <remarks>
        /// https://github.com/serilog/serilog/wiki/Enrichment
        /// </remarks>
        private static LoggerConfiguration CreateLoggerConfiguration(
            IConfiguration configuration,
            bool developmentEnvironment)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName();

            if (developmentEnvironment)
            {
                loggerConfiguration.WriteTo.Console();
            }
            else
            {
                loggerConfiguration.WriteTo.Console(new CompactJsonFormatter());
            }

            return loggerConfiguration;
        }
    }
}
