using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Builders;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Dtos;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly AppDbContext _context;

    public EmployeesController(IEmployeeService employeeService, AppDbContext context)
    {
        _employeeService = employeeService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var employees = await _employeeService.GetAll();

        if (!employees.Any())
            return BadRequest("No employees founded..");

        var employeesDto = employees.Select(e => new EmployeeDto
        {
            Address = e.Address,
            BirthDate = e.BirthDate,
            City = e.City,
            Country = e.Country,
            EmployeeId = e.EmployeeId,
            Extension = e.Extension,
            FirstName = e.FirstName,
            HireDate = e.HireDate,
            HomePhone = e.HomePhone,
            LastName = e.LastName,
            Notes = e.Notes,
            Photo = e.Photo,
            PhotoPath = e.PhotoPath,
            PostalCode = e.PostalCode,
            Region = e.Region,
            ReportsTo = string.Concat(e.ReportsToNavigation?.FirstName, ' ',
                e.ReportsToNavigation?.LastName),
            Title = e.Title,
            TitleOfCourtesy = e.TitleOfCourtesy
        });

        return Ok(employeesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var employee = await _employeeService.GetById(id);

        if (employee == null)
            return NotFound($"No employee found by id: {id}");

        var dto = new EmployeeDto
        {
            Address = employee.Address,
            LastName = employee.LastName,
            HireDate = employee.HireDate,
            BirthDate = employee.BirthDate,
            Country = employee.Country,
            City = employee.City,
            EmployeeId = employee.EmployeeId,
            Extension = employee.Extension,
            FirstName = employee.FirstName,
            HomePhone = employee.HomePhone,
            Notes = employee.Notes,
            Photo = employee.Photo,
            PhotoPath = employee.PhotoPath,
            PostalCode = employee.PostalCode,
            Region = employee.Region,
            ReportsTo = String.Concat(employee.ReportsToNavigation?.FirstName, ' ', employee.ReportsToNavigation?.LastName),
            Title = employee.Title,
            TitleOfCourtesy = employee.TitleOfCourtesy
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromForm] EmployeeModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        using var stream = new MemoryStream();
        model.Photo?.CopyToAsync(stream);

        var employee = new EmployeeBuilder()
            .SetAddress(model.Address)
            .SetCity(model.City)
            .WithTitleOfCourtesy(model.TitleOfCourtesy)
            .WithBirthDate(model.BirthDate)
            .WithCountry(model.Country)
            .WithExtension(model.Extension)
            .SetFirstName(model.FirstName)
            .SetLastName(model.LastName)
            .WithHomePhone(model.HomePhone)
            .WithHireDate(model.HireDate)
            .WithTitle(model.Title)
            .ReportsTo(model.ReportsTo)
            .SetRegion(model.Region)
            .WithPostalCode(model.PostalCode)
            .WithPhotoPath(model.PhotoPath)
            .WithNotes(model.Notes)
            .WithPhoto(stream.ToArray())
            .Build();

        await _employeeService.Create(employee);

        return Ok(employee);
    }

    [HttpGet(nameof(Paginate))]
    public async Task<IActionResult> Paginate([FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var employees = await _employeeService.GetAll();

        if (!employees.Any())
            return NotFound($"There is no any employee !!");

        if (pageSize <= 0)
            pageSize = 10;

        if (page <= 0)
            page = 1;

        var result = employees.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(e => new EmployeeDto
            {
                Address = e.Address,
                City = e.City,
                PostalCode = e.PostalCode,
                BirthDate = e.BirthDate,
                Country = e.Country,
                Extension = e.Extension,
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeId = e.EmployeeId,
                HireDate = e.HireDate,
                HomePhone = e.HomePhone,
                Notes = e.Notes,
                PhotoPath = e.PhotoPath,
                Region = e.Region,
                Title = e.Title,
                TitleOfCourtesy = e.TitleOfCourtesy,
                ReportsTo = String.Concat(e.ReportsToNavigation?.FirstName, " ", e.ReportsToNavigation?.LastName),
                Photo = e.Photo
            });

        return Ok(result);

    }

    [HttpGet(nameof(SearchEmployees))]
    public async Task<IActionResult> SearchEmployees([FromQuery] string key)
    {
        var employees = await _employeeService.GetAll();

        if (!employees.Any())
            return NotFound($"There is no any employee !!");

        var result = employees.Where(e => $"{e.FirstName} {e.LastName}".Contains(key))
            .Select(e => new EmployeeDto
            {
                Address = e.Address,
                City = e.City,
                PostalCode = e.PostalCode,
                BirthDate = e.BirthDate,
                Country = e.Country,
                Extension = e.Extension,
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeId = e.EmployeeId,
                HireDate = e.HireDate,
                HomePhone = e.HomePhone,
                Notes = e.Notes,
                PhotoPath = e.PhotoPath,
                Region = e.Region,
                Title = e.Title,
                TitleOfCourtesy = e.TitleOfCourtesy,
                ReportsTo = String.Concat(e.ReportsToNavigation?.FirstName, " ", e.ReportsToNavigation?.LastName),
                Photo = e.Photo
            }).ToList();

        return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] EmployeeModel model)
    {
        var employee = await _employeeService.GetById(id);
        if (employee == null)
            return BadRequest($"No employee found with id: {id}");

        using var stream = new MemoryStream();
        model.Photo?.CopyToAsync(stream);

        var updatedEmployee = new EmployeeBuilder(employee.EmployeeId)
                .SetAddress(model.Address)
                .SetCity(model.City)
                .WithTitleOfCourtesy(model.TitleOfCourtesy)
                .WithBirthDate(model.BirthDate)
                .WithCountry(model.Country)
                .WithExtension(model.Extension)
                .SetFirstName(model.FirstName)
                .SetLastName(model.LastName)
                .WithHomePhone(model.HomePhone)
                .WithHireDate(model.HireDate)
                .WithTitle(model.Title)
                .ReportsTo(model.ReportsTo)
                .SetRegion(model.Region)
                .WithPostalCode(model.PostalCode)
                .WithPhotoPath(model.PhotoPath)
                .WithNotes(model.Notes)
                .WithPhoto(stream.ToArray())
                .Build();

        _context.Entry(employee).State = EntityState.Detached;
        await _employeeService.Update(updatedEmployee);

        return Ok(updatedEmployee);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var employee = await _employeeService.GetById(id);

        if (employee is null)
            return NotFound($"No employee was found with id '{id}' to be deleted !!");

        await _employeeService.Delete(employee);

        return Ok(employee);
    }
}
