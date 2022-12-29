using ConsoleAppAsWorkerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using Serilog.Settings.Configuration;

// Set Environment Variable for app base directory.
Environment.SetEnvironmentVariable("APP_BASE_DIRECTORY", WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : Directory.GetCurrentDirectory());

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "_ConsoleAppAsWorkerService";
    })
    .ConfigureAppConfiguration((_, config) =>
    {
        config.SetBasePath(WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : Directory.GetCurrentDirectory());
    })
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<Worker>(); // Background service.
        //services.AddHostedService<TimedHostedService>(); // Timed hosted service
    })
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration, ConfigurationAssemblySource.AlwaysScanDllFiles))
    .Build();


await host.RunAsync();
