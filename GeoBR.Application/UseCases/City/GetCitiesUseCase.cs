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
    public async Task<ServiceResult<List<CityReadDto>>> GetCities()
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
        }).ToList();

        return ServiceResult<List<CityReadDto>>.Success(citiesDto);

      }
      catch (Exception ex)
      {
        return ServiceResult<List<CityReadDto>>.Fail($"Não foi possivel obter as cidades {ex.Message}");
      }
    }

    public async Task<ServiceResult<List<CityReadDto>>> GetCitiesByState(string state)
    {
      try
      {
        var response = await _httpClient.GetStringAsync(
          "https://servicodados.ibge.gov.br/api/v1/localidades/municipios?view=nivelado"
        );

        if (response == null)
          return ServiceResult<List<CityReadDto>>.Fail("Não foi possivel obter as cidades na API");

        var citiesFromIbge = JsonSerializer.Deserialize<List<JsonElement>>(response);

        var citiesList = citiesFromIbge.Select(c => new CityReadDto
        {
          Id = c.GetProperty("municipio-id").GetInt32(),
          Name = c.GetProperty("municipio-nome").GetString(),
          State = c.GetProperty("UF-nome").GetString(),
          Region = c.GetProperty("regiao-nome").GetString()
        }).ToList();

        var citiesDto = citiesList
          .Where(c => c.State == state)
          .ToList();

        return ServiceResult<List<CityReadDto>>.Success(citiesDto);
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

    public async Task<ServiceResult<CityReadDto>> GetCityByName(string name)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(name))
          return ServiceResult<CityReadDto>.Fail($"O nome não pode ser vazio");

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
          .FirstOrDefault(x => x.Name == name);

        if (cityDto == null)
          return ServiceResult<CityReadDto>.Fail($"Cidade de nome {name} não encontrada!");

        return ServiceResult<CityReadDto>.Success(cityDto);
      }
      catch (Exception ex)
      {
        return ServiceResult<CityReadDto>.Fail($"Não foi possivel obter as cidades na API {ex.Message}");
      }
    }
  }
}