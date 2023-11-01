namespace NorthwindAPI.Dtos;

public class TerritoryDto
{
    public string TerritoryId { get; set; } = null!;

    public string TerritoryDescription { get; set; } = null!;

    public int RegionId { get; set; }

    public string RegionDescription { get; set; } = null!;
}

