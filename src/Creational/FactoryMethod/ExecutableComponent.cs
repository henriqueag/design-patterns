namespace Creational.FactoryMethod;

internal class ExecutableComponent : IExecutableComponent
{
    public string ComponentName => "FactoryMethod";

    public void Execute()
    {
        Console.WriteLine("Estou executando a instancia de {0}", typeof(ExecutableComponent).FullName);
    }
}
