using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Contracts;
using NorthwindAPI.Dtos;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var suppliers = await _supplierService.GetAll();

        if (!suppliers.Any())
            return BadRequest("No suppliers founded !!");

        var suppliersList = suppliers.Select(x => new SupplierDto
        {
            Address = x.Address,
            City = x.City,
            CompanyName = x.CompanyName,
            ContactName = x.ContactName,
            ContactTitle = x.ContactTitle,
            Country = x.Country,
            Fax = x.Fax,
            HomePage = x.HomePage,
            Phone = x.Phone,
            PostalCode = x.PostalCode,
            Region = x.Region,
            SupplierId = x.SupplierId
        });
        return Ok(suppliersList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var supplier = await _supplierService.GetById(id);

        if (supplier == null)
            return NotFound($"No supplier found by id: {id}");

        var dto = new SupplierDto
        {
            Address = supplier.Address,
            City = supplier.City,
            CompanyName = supplier.CompanyName,
            ContactName = supplier.ContactName,
            ContactTitle = supplier.ContactTitle,
            Country = supplier.Country,
            Fax = supplier.Fax,
            HomePage = supplier.HomePage,
            Phone = supplier.Phone,
            PostalCode = supplier.PostalCode,
            Region = supplier.Region,
            SupplierId = supplier.SupplierId
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromBody] SupplierModel dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var supplier = new Supplier
        {
            Address = dto.Address,
            City = dto.City,
            CompanyName = dto.CompanyName,
            ContactName = dto.ContactName,
            ContactTitle = dto.ContactTitle,
            Country = dto.Country,
            Fax = dto.Fax,
            HomePage = dto.HomePage,
            Phone = dto.Phone,
            PostalCode = dto.PostalCode,
            Region = dto.Region
        };

        await _supplierService.Create(supplier);

        return Ok(supplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] SupplierModel model)
    {
        var supplier = await _supplierService.GetById(id);
        if (supplier == null)
            return BadRequest($"No supplier found with id: {id}");

        supplier.Address = model.Address;
        supplier.City = model.City;
        supplier.HomePage = model.HomePage;
        supplier.Phone = model.Phone;
        supplier.PostalCode = model.PostalCode;
        supplier.Region = model.Region;
        supplier.Country = model.Country;
        supplier.Fax = model.Fax;
        supplier.CompanyName = model.CompanyName;
        supplier.ContactName = model.ContactName;
        supplier.ContactTitle = model.ContactTitle;

        await _supplierService.Update(supplier);
        return Ok(supplier);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var supplier = await _supplierService.GetById(id);

        if (supplier == null)
            return NotFound($"No supplier was found with id '{id}' to be deleted !!");

        await _supplierService.Delete(supplier);

        return Ok(supplier);
    }
}
