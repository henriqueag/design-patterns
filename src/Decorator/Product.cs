using Bogus;

namespace Decorator;

public class Product
{
    private static int _nextId = 1;

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public static Faker<Product> CreateFaker()
    {
        return new Faker<Product>()
            .RuleFor(p => p.Id, f => _nextId++)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1.5M, 299))
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 20));
    }
}
