using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(int id);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category> Delete(Category category);
}
