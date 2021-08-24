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
            return new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddTransient<IExtractor, Extractor>()
                .AddTransient<IHotelManagerService, HotelManagerService>()
                .BuildServiceProvider();
        }
    }
}
