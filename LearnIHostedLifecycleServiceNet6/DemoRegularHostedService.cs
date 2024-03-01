
namespace LearnHostedLifetimeService;

public class DemoRegularHostedService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Execution the {0}", nameof(DemoRegularHostedService));
        return Task.CompletedTask;
    }
}