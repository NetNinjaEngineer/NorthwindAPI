namespace NorthwindAPI.Dtos;

public class CategoryDtoModel
{
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public IFormFile? Picture { get; set; }
}