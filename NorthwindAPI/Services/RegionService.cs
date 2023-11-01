using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class RegionService : IRegionService
{
    private readonly AppDbContext _context;

    public RegionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Region> Create(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }

    public async Task<Region> Delete(Region region)
    {
        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();
        return region;
    }

    public async Task<IEnumerable<Region>> GetAll()
    {
        var regions = await _context.Regions
            .Include(x => x.Territories)
            .ToListAsync();
        return regions;
    }

    public async Task<Region> GetById(int id)
    {
        var region = await _context.Regions
            .Include(x => x.Territories)
            .SingleOrDefaultAsync(x => x.RegionId == id);
        return region!;
    }

    public async Task<Region> Update(Region region)
    {
        _context.Regions.Update(region);
        await _context.SaveChangesAsync();
        return region;
    }
}
