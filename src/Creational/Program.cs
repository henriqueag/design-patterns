using Microsoft.Extensions.DependencyInjection;

namespace Creational;

static class Program
{
    static readonly IServiceProvider Services = ConfigureServices();

    static void Main()
    {
        ShowMenu();
        string? componentName = string.Empty;
        while (componentName is not null)
        {
            Console.Write("Opção: ");
            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                componentName = string.Empty;
                Console.Clear();

                continue;
            }

            if (option == 6)
            {
                ShowMenu();
                continue;
            }

            componentName = option switch
            {
                1 => "AbstractFactory",
                2 => "Builder",
                3 => "FactoryMethod",
                4 => "Prototype",
                5 => "Singleton",
                _ => null
            };

            Console.WriteLine();

            GetService<IEnumerable<IExecutableComponent>>()
                .FirstOrDefault(x => x.ComponentName == componentName)
                ?.Execute();

            Console.WriteLine();
        }

        Console.WriteLine("Encerrando a aplicação...");
        Environment.Exit(0);
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Selecione uma das opções abaixo para fazer a execução\n");
        var menuOptions = new[] { "AbstractFactory", "Builder", "FactoryMethod", "Prototype", "Singleton", "Menu", "Sair" };

        Console.WriteLine($"|{string.Empty.PadLeft(26, '-')}|");
        Console.WriteLine("| {0, -5} | {1, -16} |", "Opção", "Descrição");
        Console.WriteLine("| {0} | {1} |", string.Empty.PadLeft(5, '-'), string.Empty.PadLeft(16, '-'));
        for (int i = 0; i < menuOptions.Length; i++)
        {
            Console.WriteLine("| {0, -5} | {1, -16} |", i + 1, menuOptions[i]);
        }
        Console.WriteLine($"|{string.Empty.PadLeft(26, '-')}|\n");
    }

    static IServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<IExecutableComponent, AbstractFactory.ExecutableComponent>()
            .AddSingleton<IExecutableComponent, Builder.ExecutableComponent>()
            .AddSingleton<IExecutableComponent, FactoryMethod.ExecutableComponent>()
            .AddSingleton<IExecutableComponent, Prototype.ExecutableComponent>()
            .AddSingleton<IExecutableComponent, Singleton.ExecutableComponent>()
            .BuildServiceProvider();
    }

    static T GetService<T>() where T : class
    {
        return Services.GetRequiredService<T>();
    }
}