using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Contracts;
using NorthwindAPI.Dtos;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TerritoriesController : ControllerBase
{
    private readonly ITerritoryService _territoryService;

    public TerritoriesController(ITerritoryService territoryService)
    {
        _territoryService = territoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var territories = await _territoryService.GetAll();

        if (!territories.Any())
            return BadRequest("No territories founded..");

        IEnumerable<TerritoryDto> territoryDtos = territories.Select(t => new TerritoryDto
        {
            TerritoryId = t.TerritoryId,
            RegionId = t.RegionId,
            RegionDescription = t.Region.RegionDescription,
            TerritoryDescription = t.TerritoryDescription
        });

        return Ok(territoryDtos);
    }

    [HttpGet("{GetTerritory}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var territory = await _territoryService.GetById(id);

        if (territory == null)
            return NotFound($"No territory found by id: {id}");

        var dto = new TerritoryDto
        {
            RegionId = territory.RegionId,
            TerritoryId = territory.TerritoryId,
            TerritoryDescription = territory.TerritoryDescription,
            RegionDescription = territory.Region.RegionDescription
        };

        return Ok(dto);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromForm] TerritoryModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var territory = new Territory
        {
            RegionId = model.RegionId,
            TerritoryDescription = model.TerritoryDescription
        };

        await _territoryService.Create(territory);

        return Ok(territory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromForm] TerritoryModel model)
    {
        var territory = await _territoryService.GetById(id);
        if (territory == null)
            return BadRequest($"No territory found with id: {id}");

        territory.RegionId = model.RegionId;
        territory.TerritoryDescription = model.TerritoryDescription;

        await _territoryService.Update(territory);
        return Ok(territory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var territory = await _territoryService.GetById(id);

        if (territory is null)
            return NotFound($"No employee was found with id '{id}' to be deleted !!");

        await _territoryService.Delete(territory);

        return Ok(territory);
    }
}
