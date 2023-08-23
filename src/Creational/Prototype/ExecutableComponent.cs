namespace Creational.Prototype;

internal class ExecutableComponent : IExecutableComponent
{
    public string ComponentName => "Prototype";

    public void Execute()
    {
        Console.WriteLine("Estou executando a instancia de {0}", typeof(ExecutableComponent).FullName);
    }
}
