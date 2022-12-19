// See https://aka.ms/new-console-template for more information

using ConsoleAppAsWorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "_ConsoleAppAsWorkerService";
    })
    .ConfigureServices((hostServices, services) =>
    {
        //services.AddHostedService<Worker>(); // Background service.
        services.AddHostedService<TimedHostedService>(); // Timed hosted service
    })
    .Build();


await host.RunAsync();
