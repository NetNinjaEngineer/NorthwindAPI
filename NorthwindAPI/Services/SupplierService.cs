using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class SupplierService : ISupplierService
{
    private readonly AppDbContext _context;

    public SupplierService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Supplier> Create(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier> Delete(Supplier supplier)
    {
        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<IEnumerable<Supplier>> GetAll()
    {
        var suppliers = await _context.Suppliers
            .Include(x => x.Products)
            .ToListAsync();
        return suppliers;
    }

    public async Task<Supplier> GetById(int id)
    {
        var supplier = await _context.Suppliers
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.SupplierId == id);
        return supplier!;
    }

    public async Task<Supplier> Update(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }
}
