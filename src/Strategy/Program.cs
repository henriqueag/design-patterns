using Microsoft.Extensions.DependencyInjection;
using Strategy.Services;
using Strategy.Services.Strategies;

namespace Strategy;

internal static class Program
{
    private static IServiceProvider s_services = null!;

    internal static void Main(string[] args)
    {
        ConfigureServices();


        Console.ReadKey();
    }

    internal static void ConfigureServices()
    {
        s_services = new ServiceCollection()
            .AddTransient<IPaymentManager, PixPaymentStrategy>()
            .AddTransient<IPaymentManager, CashPaymentStrategy>()
            .AddTransient<IPaymentManager, CreditCardPaymentStrategy>()
            .BuildServiceProvider();
    }

    internal static T GetService<T>()
    {
        return s_services.GetRequiredService<T>();
    }
}