using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface IRegionService
{
    Task<IEnumerable<Region>> GetAll();
    Task<Region> GetById(int id);
    Task<Region> Create(Region region);
    Task<Region> Update(Region region);
    Task<Region> Delete(Region region);
}

