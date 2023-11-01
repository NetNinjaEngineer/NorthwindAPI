using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Delete(Product product);
}
