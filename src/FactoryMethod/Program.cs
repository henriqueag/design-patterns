using Microsoft.Extensions.DependencyInjection;

namespace FactoryMethod;

public class Program
{
    private static IServiceProvider Services { get; set; }

    static void Main(string[] args)
    {
        ConfigureServices();

        GetService<TruckFactory>()
            .CreateTransport()
            .Deliver();

        GetService<ShipFactory>()
            .CreateTransport()
            .Deliver();

        Console.ReadKey();
    }

    static void ConfigureServices()
    {
        var services = new ServiceCollection();

        typeof(ITransportFactory).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && x.IsAssignableFrom(x))
            .ToList()
            .ForEach(t => services.AddTransient(t));

        Services = services.BuildServiceProvider();
    }

    static T GetService<T>() => Services.GetRequiredService<T>();
}
