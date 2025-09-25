namespace GeoBR.Application.DTOs
{
  public class CityFilterDto
  {
    public string? State { get; set; }
    public string? Name { get; set; }
    public string? Region { get; set; }
    public int? Limit { get; set; }
  }
}