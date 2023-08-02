namespace Decorator;

public interface IProductDbContext
{
    IDictionary<int, Product> Products { get; }
}

public class ProductDbContext : IProductDbContext
{
    public IDictionary<int, Product> Products { get; }

    public ProductDbContext()
    {
        Products = Product.CreateFaker()
            .Generate(2_000)
            .ToDictionary(keySelector => keySelector.Id, valueSelector => valueSelector);
    }
}
