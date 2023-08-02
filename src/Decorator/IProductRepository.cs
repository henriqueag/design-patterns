using Microsoft.Extensions.Caching.Memory;

namespace Decorator;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}

public class ProductRepository : IProductRepository
{
    private readonly IProductDbContext _dbContext;

    public ProductRepository(IProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Product> GetAll()
    {
        Task.Delay(2000).GetAwaiter().GetResult();

        return _dbContext.Products.Values;
    }

    public Product? GetById(int id) => _dbContext.Products.TryGetValue(id, out var product) ? product : null; 
}

public class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _repo;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedProductRepository> _logger;

    public CachedProductRepository(IProductRepository repo, IMemoryCache cache, ILogger<CachedProductRepository> logger)
    {
        _repo = repo;
        _cache = cache;
        _logger = logger;
    }

    public IEnumerable<Product> GetAll()
    {
        var key = $"{nameof(CachedProductRepository)}.all-items";
        if(_cache.TryGetValue<IEnumerable<Product>>(key, out var fromCache))
        {
            _logger.LogInformation("A lista de produtos foi recuperada do cache");

            return fromCache!;
        }

        var products = _repo.GetAll();
        _cache.Set(key, products, TimeSpan.FromSeconds(30));

        _logger.LogInformation("A lista de produtos foi adicionada no cache");

        return products;
    }

    public Product? GetById(int id)
    {
        var key = $"{nameof(CachedProductRepository)}.by-id.{id}";
        if (_cache.TryGetValue<Product>(key, out var fromCache))
        {
            _logger.LogInformation("O produto {@Product} foi recuperado do cache", 
                new { fromCache!.Id, fromCache.Name });

            return fromCache!;
        }

        var product = _repo.GetById(id);
        _cache.Set(key, product, TimeSpan.FromSeconds(30));

        _logger.LogInformation("O produto {@Product} foi adicionado no cache",
                new { product.Id, product.Name });

        return product;
    }
}