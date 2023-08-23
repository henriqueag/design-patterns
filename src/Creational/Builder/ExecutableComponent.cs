namespace Creational.Builder;

internal class ExecutableComponent : IExecutableComponent
{
    public string ComponentName => "Builder";

    public void Execute()
    {
        Console.WriteLine("Estou executando a instancia de {0}", typeof(ExecutableComponent).FullName);
    }
}
