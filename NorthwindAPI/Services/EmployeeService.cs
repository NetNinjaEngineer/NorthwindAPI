using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> Create(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        var employees = await _context.Employees
            .Include(x => x.InverseReportsToNavigation)
            .Include(x => x.Orders)
            .Include(x => x.Territories)
            .Include(x => x.ReportsToNavigation)
            .ToListAsync();
        return employees;
    }

    public async Task<Employee> GetById(int id)
    {
        var employee = await _context.Employees
            .Include(x => x.InverseReportsToNavigation)
            .Include(x => x.Orders)
            .Include(x => x.Territories)
            .Include(x => x.ReportsToNavigation)
            .SingleOrDefaultAsync(x => x.EmployeeId == id);
        return employee!;
    }

    public async Task<Employee> Update(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
}
