using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> Create(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Delete(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        var categories = await _context.Categories
            .Include(x => x.Products)
            .ToListAsync();
        return categories;
    }

    public async Task<Category> GetById(int id)
    {
        var category = await _context.Categories
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.CategoryId == id);
        return category!;
    }

    public async Task<Category> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
