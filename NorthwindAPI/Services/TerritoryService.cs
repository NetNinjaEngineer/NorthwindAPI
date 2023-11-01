using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class TerritoryService : ITerritoryService
{
    private readonly AppDbContext _context;

    public TerritoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Territory> Create(Territory territory)
    {
        await _context.Territories.AddAsync(territory);
        await _context.SaveChangesAsync();
        return territory;
    }

    public async Task<Territory> Delete(Territory territory)
    {
        _context.Territories.Remove(territory);
        await _context.SaveChangesAsync();
        return territory;
    }

    public async Task<IEnumerable<Territory>> GetAll()
    {
        var territories = await _context.Territories
            .Include(x => x.Employees)
            .Include(x => x.Region)
            .ToListAsync();
        return territories;
    }

    public async Task<Territory> GetById(string id)
    {
        var territory = await _context.Territories
            .Include(x => x.Employees)
            .Include(x => x.Region)
            .SingleOrDefaultAsync(x => x.TerritoryId == id);
        return territory!;
    }

    public async Task<Territory> Update(Territory territory)
    {
        _context.Territories.Update(territory);
        await _context.SaveChangesAsync();
        return territory;
    }
}