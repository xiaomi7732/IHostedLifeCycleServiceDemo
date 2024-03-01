
namespace LearnIServer;

public class MyHost : IHost
{
    private readonly IHost _host;

    public MyHost(IHost host) => _host = host ?? throw new ArgumentNullException(nameof(host));

    public IServiceProvider Services => _host.Services;

    public void Dispose()
    {
        _host.Dispose();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Injecting async code here before start...");
        await _host.StartAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return _host.StopAsync(cancellationToken);
    }
}