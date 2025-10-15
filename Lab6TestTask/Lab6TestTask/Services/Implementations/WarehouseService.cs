using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>\

public async Task<Warehouse> GetWarehouseAsync()
{
    Warehouse wHouseMaxSum = await (from prod in _dbContext.Products
                                    where prod.Status == ProductStatus.ReadyForDistribution
                                    group prod by prod.WarehouseId into gr
                                    let totalValue = gr.Sum(prod => prod.Price * prod.Quantity)
                                    orderby totalValue descending
                                    select gr.Key into warehouseId
                                    join wHouse in _dbContext.Warehouses on warehouseId equals wHouse.Id
                                    select wHouse
                                    ).FirstOrDefaultAsync();

    return wHouseMaxSum;
}

// TODO:Сделать перегрузку на первый метод с выходом списка

public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
{
    List<Warehouse> wHouseSecQuarter = await (from prod in _dbContext.Products
                                              where prod.DeliveryDate >= new DateTime(2025, 4, 1) && prod.DeliveryDate <= new DateTime(2025, 6, 30)
                                              select prod.Warehouse).Distinct().ToListAsync();

    return wHouseSecQuarter;
}

public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
       throw new NotImplementedException();
    }
}
