namespace DependencyInjection;

internal class SingletonService
{
    public int Contador { get; private set; } = 0;

    public void Incrementar() => Contador++;
}

internal abstract class ClientSingletonServiceBase
{
    private readonly SingletonService _service;
    protected readonly Serilog.ILogger Logger;

    protected ClientSingletonServiceBase(SingletonService service, Serilog.ILogger logger)
    {
        _service = service;
        Logger = logger;
    }

    protected void Print(string className)
    {
        Logger.Information("Incrementando o contador do serviço singleton usando a classe {0}", className);
        Logger.Information("Valor do contador: {0}", _service.Contador);
    }

    public virtual void Execute()
    {
        _service.Incrementar();
    }
}

internal class ClientSingletonService1 : ClientSingletonServiceBase
{
    public ClientSingletonService1(SingletonService service, Serilog.ILogger logger) : base(service, logger) { }

    public override void Execute()
    {
        Print(nameof(ClientSingletonService1));
        base.Execute();
    }
}
