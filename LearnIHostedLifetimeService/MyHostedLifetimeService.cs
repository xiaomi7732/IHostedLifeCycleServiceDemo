
namespace LearnHostedLifetimeService;

public class MyHostedLifetimeService(OutputService outputService) : IHostedLifecycleService
{
    private readonly OutputService _outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;

    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting async. This is expected to be called before {0}. Calling injected output service:", nameof(DemoRegularHostedService));
        _outputService.Output("Hello injected service...");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}