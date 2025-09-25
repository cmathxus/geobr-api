using System.Text.Json;
using AutoMapper;
using GeoBR.Application.DTOs;

namespace GeoBR.Application.UseCases
{
  public class GetCitiesUseCase : IGetCitiesUseCase
  {
    private readonly HttpClient _httpClient;

    public GetCitiesUseCase(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<ServiceResult<List<CityReadDto>>> GetCities(CityFilterDto city)
    {
      try
      {
        var response = await _httpClient.GetStringAsync(
          "https://servicodados.ibge.gov.br/api/v1/localidades/municipios?view=nivelado"
        );

        if (response == null)
          return ServiceResult<List<CityReadDto>>.Fail("Não foi possivel obter as cidades na API");

        var citiesFromIbge = JsonSerializer.Deserialize<List<JsonElement>>(response);

        var citiesDto = citiesFromIbge.Select(c => new CityReadDto
        {
          Id = c.GetProperty("municipio-id").GetInt32(),
          Name = c.GetProperty("municipio-nome").GetString(),
          State = c.GetProperty("UF-nome").GetString(),
          Region = c.GetProperty("regiao-nome").GetString()
        }).AsQueryable();

        if (!string.IsNullOrEmpty(city?.Name))
          citiesDto = citiesDto.Where(c => c.Name.Contains(city.Name, StringComparison.OrdinalIgnoreCase));


        if (!string.IsNullOrEmpty(city?.State))
          citiesDto = citiesDto.Where(c => c.State.Equals(city.State, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(city?.Region))
          citiesDto = citiesDto.Where(c => c.Region.Equals(city.Region, StringComparison.OrdinalIgnoreCase));

        return ServiceResult<List<CityReadDto>>.Success(citiesDto.ToList());

      }
      catch (Exception ex)
      {
        return ServiceResult<List<CityReadDto>>.Fail($"Não foi possivel obter as cidades {ex.Message}");
      }
    }

    public async Task<ServiceResult<CityReadDto>> GetCitiesById(int id)
    {
      try
      {
        if (id <= 0)
          return ServiceResult<CityReadDto>.Fail("ID informado inválido");

        var response = await _httpClient.GetStringAsync(
          "https://servicodados.ibge.gov.br/api/v1/localidades/municipios?view=nivelado"
        );

        if (response == null)
          return ServiceResult<CityReadDto>.Fail("Não foi possivel obter as cidades na API");

        var citiesFromIbge = JsonSerializer.Deserialize<List<JsonElement>>(response);

        var cityDto = citiesFromIbge
          .Select(c => new CityReadDto
          {
            Id = c.GetProperty("municipio-id").GetInt32(),
            Name = c.GetProperty("municipio-nome").GetString(),
            State = c.GetProperty("UF-nome").GetString(),
            Region = c.GetProperty("regiao-nome").GetString()
          })
          .FirstOrDefault(x => x.Id == id);

        if (cityDto == null)
          return ServiceResult<CityReadDto>.Fail("Cidade não encontrada");

        return ServiceResult<CityReadDto>.Success(cityDto);
      }
      catch (Exception ex)
      {
        return ServiceResult<CityReadDto>.Fail($"Não foi possivel obter a cidades {ex.Message}");
      }
    }

  }
}