namespace Creational.Singleton;

internal class ExecutableComponent : IExecutableComponent
{
    public string ComponentName => "Singleton";

    public void Execute()
    {
        Console.WriteLine("Estou executando a instancia de {0}", typeof(ExecutableComponent).FullName);
    }
}
