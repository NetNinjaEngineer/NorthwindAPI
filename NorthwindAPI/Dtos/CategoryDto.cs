namespace NorthwindAPI.Dtos;

public class CategoryDto
{
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public IFormFile? Picture { get; set; }
}
