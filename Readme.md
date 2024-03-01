# Run asynchronous code before hosting get started

## The goal

To run some async code before the host get started.

There has been post talking about various techniques before `Microsoft.Extensions.Hosting` version `8.0.0`. For example: <https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/>.

The problem was, it was kind of hard to:

1. Find a sweet spot to run the code;
    * And by sweet spot, I mean, after all the services in the dependency injection containers are registered but before any hosting code's running.
1. Run asynchronous but avoid async over sync.

## The solution

Now, everything becomes much easier as long as `Microsoft.Extensions.Hosting` is referenced - and yes, it works with .NET 6 applications too.

Follow these steps:

1. Refer to the package `Microsoft.Extensions.Hosting`;
    * You don't need to do this manually if you are running a .NET 8 application because its already there.
1. Create a class that implements `IHostedLifecycleService`. There are several methods to be implemented. Related to our goal is `StartingAsync`, which runs before any of the regular `IHostedService`. For example:

    ```csharp
    namespace LearnHostedLifetimeService;

    public class MyHostedLifetimeService : IHostedLifecycleService
    {

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting async. This is expected to be called before {0}.", nameof(DemoRegularHostedService));
            return Task.CompletedTask;
        }

        ...
    }
    ```

1. Optionally, we can add a regular `Background` service and check out the running sequence by ourselves:

    ```csharp
    namespace LearnHostedLifetimeService;

    public class DemoRegularHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Execution the {0}", nameof(DemoRegularHostedService));
            return Task.CompletedTask;
        }
    }
    ```

1. Register the hosted lifecycle along with other hosted services:

    ```csharp
    builder.Services.AddHostedService<DemoRegularHostedService>(); // Although this is registered earlier, it still executes later.
    builder.Services.AddHostedService<MyHostedLifetimeService>(); // The StartingAsync will be called first.
    ```