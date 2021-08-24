using FileExtractor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.HotelServices;
using Services.Interfecs;
using System;

namespace Parser
{
    public class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                 .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddTransient<ILogger>()
                .AddTransient<IExtractor, Extractor>()
                .AddTransient<IHotelManagerService, HotelManagerService>()

                .BuildServiceProvider();

            return provider;
        }
    }
}
