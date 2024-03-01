namespace LearnHostedLifetimeService;

/// <summary>
/// A regular service that could be injected into the hosted lifecycle service.
/// </summary>
public class OutputService
{
    public void Output(string text)
    {
        Console.WriteLine("From output service: {0}", text);
    }
}