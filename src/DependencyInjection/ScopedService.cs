namespace DependencyInjection;

internal class ScopedService
{
    public int Contador { get; private set; } = 0;

    public void Incrementar() => Contador++;
}

internal abstract class ClientServiceBase
{
    private readonly ScopedService _service;
    protected readonly Serilog.ILogger Logger;

    protected ClientServiceBase(ScopedService service, Serilog.ILogger logger)
    {
        _service = service;
        Logger = logger;
    }

    protected void Print(string className)
    {
        Logger.Information("Incrementando o contador do serviço scoped usando a classe {0}", className);
        Logger.Information("Valor do contador: {0}", _service.Contador);
    }

    public virtual void Execute()
    {
        _service.Incrementar();
    }
}

internal class ClientService1 : ClientServiceBase
{
    public ClientService1(ScopedService service, Serilog.ILogger logger) : base(service, logger) { }

    public override void Execute()
    {
        Print(nameof(ClientService1));
        base.Execute();
    }
}

internal class ClientService2 : ClientServiceBase
{
    public ClientService2(ScopedService service, Serilog.ILogger logger) : base(service, logger) { }

    public override void Execute()
    {
        Print(nameof(ClientService2));
        base.Execute();
    }
}

internal class ClientService3 : ClientServiceBase
{
    public ClientService3(ScopedService service, Serilog.ILogger logger) : base(service, logger) { }

    public override void Execute()
    {
        Print(nameof(ClientService3));
        base.Execute();
    }
}