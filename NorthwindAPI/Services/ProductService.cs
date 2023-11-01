using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    private async Task<bool> CheckSupplierAndCategory(Product product)
    {
        var validSupplierId = await _context.Products.AnyAsync(x => x.SupplierId == product.SupplierId);
        var validCategoryId = await _context.Products.AnyAsync(x => x.CategoryId == product.CategoryId);

        return (validSupplierId && validCategoryId);

    }

    public async Task<Product> Create(Product product)
    {
        bool valid = await CheckSupplierAndCategory(product);
        if (!valid) return null!;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .Include(x => x.OrderDetails)
            .ToListAsync();
        return products;
    }

    public async Task<Product> GetById(int id)
    {
        var product = await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .Include(x => x.OrderDetails)
            .SingleOrDefaultAsync(x => x.ProductId == id);
        return product!;
    }

    public async Task<Product> Update(Product product)
    {
        bool valid = await CheckSupplierAndCategory(product);
        if (!valid) return null!;

        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return product;
    }
}
