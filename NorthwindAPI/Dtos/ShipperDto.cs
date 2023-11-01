namespace NorthwindAPI.Dtos;

public class ShipperDto
{
    public int ShipperId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }
}
