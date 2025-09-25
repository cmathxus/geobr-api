using System.Text.Json.Serialization;

namespace GeoBR.Application.DTOs
{
  public class CityReadDto
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("municipio")]
    public string Name { get; set; }

    [JsonPropertyName("estado")]
    public string State { get; set; }

    [JsonPropertyName("regiao")]
    public string Region { get; set; }
  }
}