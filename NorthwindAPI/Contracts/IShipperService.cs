using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface IShipperService
{
    Task<IEnumerable<Shipper>> GetAll();
    Task<Shipper> GetById(int id);
    Task<Shipper> Create(Shipper shipper);
    Task<Shipper> Update(Shipper shipper);
    Task<Shipper> Delete(Shipper shipper);
}