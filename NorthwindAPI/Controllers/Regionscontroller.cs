using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Contracts;
using NorthwindAPI.Dtos;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Regionscontroller : ControllerBase
{
    private readonly IRegionService _regionService;

    public Regionscontroller(IRegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var regions = await _regionService.GetAll();

        if (!regions.Any())
            return BadRequest("No Regions founded..");

        IEnumerable<RegionDto> regionsDtos = regions.Select(t => new RegionDto
        {
            RegionId = t.RegionId,
            RegionDescription = t.RegionDescription
        });

        return Ok(regionsDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var region = await _regionService.GetById(id);

        if (region == null)
            return NotFound($"No region found by id: {id}");

        var dto = new RegionDto
        {
            RegionId = region.RegionId,
            RegionDescription = region.RegionDescription
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromBody] RegionModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var region = new Region
        {
            RegionDescription = model.RegionDescription
        };

        await _regionService.Create(region);

        return Ok(region);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] RegionModel model)
    {
        var region = await _regionService.GetById(id);
        if (region == null)
            return BadRequest($"No region found with id: {id}");

        region.RegionDescription = model.RegionDescription;

        await _regionService.Update(region);
        return Ok(region);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var region = await _regionService.GetById(id);

        if (region is null)
            return NotFound($"No region was found with id '{id}' to be deleted !!");

        await _regionService.Delete(region);

        return Ok(region);
    }
}
