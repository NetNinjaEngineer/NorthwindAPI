using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAll();
    Task<Supplier> GetById(int id);
    Task<Supplier> Create(Supplier supplier);
    Task<Supplier> Update(Supplier supplier);
    Task<Supplier> Delete(Supplier supplier);
}