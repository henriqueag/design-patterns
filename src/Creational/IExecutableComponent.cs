namespace Creational;

internal interface IExecutableComponent
{
    string ComponentName { get; }

    void Execute();
}
