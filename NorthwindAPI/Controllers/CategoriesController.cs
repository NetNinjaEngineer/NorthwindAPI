using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Contracts;
using NorthwindAPI.Data;
using NorthwindAPI.Dtos;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly AppDbContext _context;

    public CategoriesController(ICategoryService categoryService, AppDbContext context)
    {
        _categoryService = categoryService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryService.GetAll();

        if (!categories.Any())
            return BadRequest("No categories founded");

        var categoriesDto = new List<CategoryModel>();

        foreach (var category in categories)
        {
            categoriesDto.Add(new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture
            });
        }
        return Ok(categoriesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var category = await _categoryService.GetById(id);

        if (category is null)
            return NotFound($"No category found by id: {id}");

        var dto = new CategoryModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        };

        return Ok(dto);
    }


    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync([FromForm] CategoryDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        using var dataStream = new MemoryStream();
        model.Picture?.CopyToAsync(dataStream);

        var category = new Category
        {
            CategoryName = model.CategoryName,
            Description = model.Description,
            Picture = dataStream.ToArray()
        };

        await _categoryService.Create(category);

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] CategoryDtoModel categoryDtoModel)
    {
        var category = await _categoryService.GetById(id);
        if (category == null)
            return BadRequest($"No category found with id: {id}");

        if (categoryDtoModel.Picture is not null)
        {
            using var stream = new MemoryStream();
            await categoryDtoModel.Picture!.CopyToAsync(stream);
            category.Picture = stream.ToArray();
        }

        category.CategoryName = categoryDtoModel.CategoryName;
        category.Description = categoryDtoModel.Description;

        await _categoryService.Update(category);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var category = await _categoryService.GetById(id);

        if (category == null)
            return NotFound($"No category was found with id '{id}' to be deleted !!");

        await _categoryService.Delete(category);

        return Ok(category);
    }

}
