using GeoBR.Application.DTOs;
using GeoBR.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GeoBR.API.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class GetCitiesController : ControllerBase
  {
    private readonly IGetCitiesUseCase _getCitiesUseCase;

    public GetCitiesController(IGetCitiesUseCase getCitiesUseCase)
    {
      _getCitiesUseCase = getCitiesUseCase;
    }

    /// <summary>
    /// Retorna todas as cidades ou filtra por estado/nome/regiao.
    /// </summary>
    /// Filtros opcionais: name, state, region e limites

    [HttpGet("/cities")]
    public async Task<IActionResult> GetCities([FromQuery] CityFilterDto city)
    {
      var result = await _getCitiesUseCase.GetCities(city);

      if (!result.Result)
        return BadRequest(result.Message);

      return Ok(result.Data);
    }

    /// <summary>
    /// Retorna uma cidade pelo ID (IBGE).
    /// </summary>
    /// <param name="id">ID da cidade no IBGE</param>
    [HttpGet("/cities/{id}")]
    public async Task<IActionResult> GetCityById(int id)
    {
      var result = await _getCitiesUseCase.GetCitiesById(id);

      if (!result.Result)
        return BadRequest(result.Message);

      return Ok(result.Data);
    }
  }
}