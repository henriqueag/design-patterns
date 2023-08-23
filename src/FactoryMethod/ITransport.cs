namespace FactoryMethod;

internal interface ITransportFactory
{
    ITransport CreateTransport();
}

internal class TruckFactory : ITransportFactory
{
    public ITransport CreateTransport()
    {
        return new Truck();
    }
}

internal class ShipFactory : ITransportFactory
{
    public ITransport CreateTransport()
    {
        return new Ship();
    }
}

internal interface ITransport
{
    void Deliver();
}

internal class Truck : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("O transporte está sendo feito por transporte terrestre");
    }
}

internal class Ship : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("O transporte está sendo feito por transporte naval");
    }
}
