using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Contracts;
using NorthwindAPI.Dtos;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippersController : ControllerBase
{
    private readonly IShipperService _shipperService;

    public ShippersController(IShipperService shipperService)
    {
        _shipperService = shipperService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var shippers = await _shipperService.GetAll();

        if (!shippers.Any())
            return BadRequest("No shippers founded..");

        IEnumerable<ShipperDto> shipperDtos = shippers.Select(t => new ShipperDto
        {
            ShipperId = t.ShipperId,
            CompanyName = t.CompanyName,
            Phone = t.Phone
        });

        return Ok(shipperDtos);
    }

    [HttpGet("{GetShipper}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var shipper = await _shipperService.GetById(id);

        if (shipper == null)
            return NotFound($"No shipper found by id: {id}");

        var dto = new ShipperDto
        {
            Phone = shipper.Phone,
            CompanyName = shipper.CompanyName,
            ShipperId = shipper.ShipperId
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromBody] ShipperModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var shipper = new Shipper
        {
            CompanyName = model.CompanyName,
            Phone = model.Phone
        };

        await _shipperService.Create(shipper);

        return Ok(shipper);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ShipperModel model)
    {
        var shipper = await _shipperService.GetById(id);
        if (shipper == null)
            return BadRequest($"No shipper found with id: {id}");

        shipper.CompanyName = model.CompanyName;
        shipper.Phone = model.Phone;

        await _shipperService.Update(shipper);
        return Ok(shipper);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var shipper = await _shipperService.GetById(id);

        if (shipper is null)
            return NotFound($"No shipper was found with id '{id}' to be deleted !!");

        await _shipperService.Delete(shipper);

        return Ok(shipper);
    }
}
