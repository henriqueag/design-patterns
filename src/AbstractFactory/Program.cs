using AbstractFactory;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryMethod;

public static class Program
{
    private static IServiceProvider Services { get; set; }

    static void Main()
    {
        ConfigureServices();

        IServiceScope scope;
        scope = Services.CreateScope();

        var factory = scope.ServiceProvider.GetService<IOperationFactory>();
        var schemaOp = factory.New<GetSchemaOperation.GetSchemaArgs, Schema>();
        Schema result = schemaOp.Execute(new GetSchemaOperation.GetSchemaArgs { Connector = new(), CreatedAt = DateTime.Now });

        Console.WriteLine();
        Console.WriteLine(result.Name);

        Task.Delay(1500).GetAwaiter().GetResult();

        result = schemaOp.Execute(new GetSchemaOperation.GetSchemaArgs { Connector = new(), CreatedAt = DateTime.Now });

        Console.WriteLine(result.Name);

        scope.Dispose();


        Console.ReadKey();
    }

    static void ConfigureServices()
    {
        Services = new ServiceCollection()
            .AddSingleton<IOperationFactory, OperationFactory>()
            .AddTransient<IOperation<GetDataOperation.GetDataArgs, object>, GetDataOperation>()
            .AddScoped<IOperation<GetSchemaOperation.GetSchemaArgs, Schema>, GetSchemaOperation>()
            .BuildServiceProvider();
    }

    static T GetService<T>() => Services.GetRequiredService<T>();
}
