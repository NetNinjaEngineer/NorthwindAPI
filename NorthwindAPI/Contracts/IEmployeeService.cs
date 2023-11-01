using NorthwindAPI.Entities;

namespace NorthwindAPI.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee> GetById(int id);
    Task<Employee> Create(Employee employee);
    Task<Employee> Update(Employee employee);
    Task<Employee> Delete(Employee employee);
}