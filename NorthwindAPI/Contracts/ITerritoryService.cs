using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface ITerritoryService
{
    Task<IEnumerable<Territory>> GetAll();
    Task<Territory> GetById(string id);
    Task<Territory> Create(Territory territory);
    Task<Territory> Update(Territory territory);
    Task<Territory> Delete(Territory territory);
}