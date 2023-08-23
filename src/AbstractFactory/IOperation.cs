using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace AbstractFactory;

internal static class ObjectExtensions
{
    public static string ToJson(this object source)
    {
        return JsonSerializer.Serialize(source);
    }
}

internal class Connector
{
    public Guid Id { get; } = Guid.NewGuid();
}

internal interface IOperationArgs
{
    Connector Connector { get; }
}

internal interface IOperation<in TArgs, out TResult>
    where TArgs : IOperationArgs
{
    TResult Execute(TArgs args);
}

internal interface IOperationFactory
{
    IOperation<TArgs, TResult> New<TArgs, TResult>() where TArgs : IOperationArgs;
}

internal class OperationFactory : IOperationFactory
{
    private readonly IServiceProvider _services;

    public OperationFactory(IServiceProvider services) => _services = services; 

    public IOperation<TArgs, TResult> New<TArgs, TResult>() where TArgs : IOperationArgs
    {
        var operation = _services.GetRequiredService<IOperation<TArgs, TResult>>()
            ?? throw new InvalidOperationException();

        return operation;
    }
}

internal class GetDataOperation : IOperation<GetDataOperation.GetDataArgs, object>
{
    public object Execute(GetDataArgs args)
    {
        Console.WriteLine("Execução da operação {0} com os argumentos {1}",
            nameof(GetDataOperation), args.ToJson());

        return new
        {
            Message = $"Executei a operação {nameof(GetDataOperation)}"
        };
    }

    internal class GetDataArgs : IOperationArgs
    {
        public required Connector Connector { get; init; }
        public required string Name { get; init; }
    }
}

internal class Schema
{
    public string Name { get; set; }
}

internal class GetSchemaOperation : IOperation<GetSchemaOperation.GetSchemaArgs, Schema>
{
    private Schema _schema;

    public Schema Execute(GetSchemaArgs args)
    {
        if (_schema is not null)
        {
            Console.WriteLine("O schema retornado estava armazenado em cache de escopo");
            return _schema;
        }

        Console.WriteLine("Execução da operação {0} com os argumentos {1}",
            nameof(GetDataOperation), args.ToJson());

        _schema = new Schema() { Name = args.CreatedAt.ToString() };

        return _schema;
    }

    internal class GetSchemaArgs : IOperationArgs
    {
        public required Connector Connector { get; init; }
        public required DateTime CreatedAt { get; init; }
    }
}