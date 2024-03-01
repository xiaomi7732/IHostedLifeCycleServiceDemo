using LearnHostedLifetimeService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<DemoRegularHostedService>(); // Although this is registered earlier, it still executes later.
builder.Services.AddHostedService<MyHostedLifetimeService>();
builder.Services.AddSingleton<OutputService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
