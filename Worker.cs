using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleAppAsWorkerService;
public class Worker : BackgroundService
{
    private int _runInterval;
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var configuration = _serviceScopeFactory.CreateScope().
            ServiceProvider.GetRequiredService<IConfiguration>();
        _runInterval = int.Parse(configuration["TimeInterval"] ?? "0");
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromSeconds(_runInterval), stoppingToken);
        }
    }
}