using LearnIServer;
using Microsoft.Extensions.DependencyInjection.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

ServiceDescriptor? target = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IHost));
if (target is not null)
{
    Console.WriteLine("Found IHost, lifetime: {0}", target.Lifetime);
    IHost host = builder.Services.BuildServiceProvider().GetRequiredService<IHost>();
    builder.Services.Remove(target);
    builder.Services.TryAddSingleton<IHost>(_ => host);
}

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
