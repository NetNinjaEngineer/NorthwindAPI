using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class ShipperService : IShipperService
{
    private readonly AppDbContext _context;

    public ShipperService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Shipper> Create(Shipper shipper)
    {
        await _context.Shippers.AddAsync(shipper);
        await _context.SaveChangesAsync();
        return shipper;
    }

    public async Task<Shipper> Delete(Shipper shipper)
    {
        _context.Shippers.Remove(shipper);
        await _context.SaveChangesAsync();
        return shipper;
    }

    public async Task<IEnumerable<Shipper>> GetAll()
    {
        var shippers = await _context.Shippers
            .Include(x => x.Orders)
            .ToListAsync();
        return shippers;
    }

    public async Task<Shipper> GetById(int id)
    {
        var shipper = await _context.Shippers
            .Include(x => x.Orders)
            .SingleOrDefaultAsync(x => x.ShipperId == id);
        return shipper!;
    }

    public async Task<Shipper> Update(Shipper shipper)
    {
        _context.Shippers.Update(shipper);
        await _context.SaveChangesAsync();
        return shipper;
    }
}
