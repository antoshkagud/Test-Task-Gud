using Lab6TestTask.Data;
using Lab6TestTask.Enums;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>

public async Task<Product> GetProductAsync()
{
    Product expensiveProduct = await (from prod in _dbContext.Product
                                      where prod.Status == ProductStatus.Reserved
                                      orderby prod.Price descending
                                      select prod).FirstOrDefaultAsync();
    return expensiveProduct;
}

// TODO:Сделать перегрузку на первый метод с выходом списка

public async Task<IEnumerable<Product>> GetProductsAsync()
{
    List<Product> product2025 = await (from prod in _dbContext.Product
                                       where prod.ReceivedDate.Year == 2025 && prod.Quantity > 1000
                                       orderby prod.Price descending
                                       select prod).toListAsync();
    return product2025;
}

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetProductAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}
