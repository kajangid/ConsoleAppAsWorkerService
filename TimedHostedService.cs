using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleAppAsWorkerService;
public class TimedHostedService : IHostedService, IDisposable
{
    private int _runInterval;
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer _timer = null!;

    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service is starting.");

        var configuration = _serviceScopeFactory.CreateScope().
            ServiceProvider.GetRequiredService<IConfiguration>();
        _runInterval = int.Parse(configuration["TimeInterval"] ?? "0");


        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_runInterval));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async void DoWork(object? state)
    {
        _logger.LogInformation("Code-Maze Service running at: {time}", DateTimeOffset.Now);

        var path = Path.Combine("D:\\Projects\\VS22\\ConsoleApp\\ConsoleAppAsWorkerService", "file.txt");
        if (!File.Exists(path))
        {
            var fs = File.Create(path);
            fs.Close();

        }
        var content = await File.ReadAllTextAsync(path);
        content = content + Environment.NewLine + $"Code-Maze Service running at: {DateTimeOffset.Now} ::: {_runInterval}";
        await File.WriteAllTextAsync(path, content);
    }
}