namespace Creational.AbstractFactory;

internal class ExecutableComponent : IExecutableComponent
{
    public string ComponentName => "AbstractFactory";

    public void Execute()
    {
        Console.WriteLine("Estou executando a instancia de {0}", typeof(ExecutableComponent).FullName);
    }
}
