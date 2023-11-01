using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Builders;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Dtos;

namespace NorthwindAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly AppDbContext _context;
    public ProductsController(IProductService productService, AppDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _productService.GetAll();

        if (!products.Any())
            return BadRequest("No products founded");

        var productsDto = products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.CategoryName,
            CompanyName = p.Supplier?.CompanyName,
            Discontinued = p.Discontinued,
            ProductName = p.ProductName,
            QuantityPerUnit = p.QuantityPerUnit,
            ReorderLevel = p.ReorderLevel,
            SupplierId = p.SupplierId,
            UnitPrice = p.UnitPrice,
            UnitsInStock = p.UnitsInStock,
            UnitsOnOrder = p.UnitsOnOrder
        });

        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
            return NotFound($"No product found by id: {id}");

        var dto = new ProductDto
        {
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.CategoryName,
            CompanyName = product.Supplier?.CompanyName,
            Discontinued = product.Discontinued,
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            QuantityPerUnit = product.QuantityPerUnit,
            ReorderLevel = product.ReorderLevel,
            SupplierId = product.SupplierId,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            UnitsOnOrder = product.UnitsOnOrder
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromBody] ProductModel model)
    {
        var product = new ProductBuilder()
            .SetProductName(model.ProductName)
            .WithQuantityPerUnit(model.QuantityPerUnit)
            .WithCategoryId(model.CategoryId)
            .WithUnitPrice(model.UnitPrice)
            .WithReorderLevel(model.ReorderLevel)
            .WithSupplierId(model.SupplierId)
            .WithUnitsOnOrder(model.UnitsOnOrder)
            .WithUnitsInStock(model.UnitsInStock)
            .Discontinued(model.Discontinued)
            .Build();

        var _createdProduct = await _productService.Create(product);

        return (_createdProduct == null) ? BadRequest("Invalid category or supplier.") : Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductModel dto)
    {
        var product = await _productService.GetById(id);
        if (product == null)
            return BadRequest($"No product found with id: {id}");

        var updatedProduct = new ProductBuilder(product.ProductId)
            .WithCategoryId(dto.CategoryId)
            .SetProductName(dto.ProductName)
            .WithQuantityPerUnit(dto.QuantityPerUnit)
            .WithReorderLevel(dto.ReorderLevel)
            .WithSupplierId(dto.SupplierId)
            .WithUnitPrice(dto.UnitPrice)
            .WithUnitsInStock(dto.UnitsInStock)
            .WithUnitsOnOrder(dto.UnitsOnOrder)
            .Build();

        _context.Entry(product).State = EntityState.Detached;

        var result = await _productService.Update(updatedProduct);

        return (result == null) ? BadRequest("Invalid category or supplier.") : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var del = await _productService.GetById(id);

        if (del == null)
            return NotFound($"No product was found with id '{id}' to be deleted !!");

        await _productService.Delete(del);

        return Ok(del);
    }
}