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
    /// Retorna todas as cidades do Brasil.
    /// </summary>

    [HttpGet("/cities")]
    public async Task<IActionResult> GetCities()
    {
      var result = await _getCitiesUseCase.GetCities();

      if (!result.Result)
        return BadRequest(result.Message);

      return Ok(result.Data);
    }


    /// <summary>
    /// Retorna as cidades pelo nome do estado (IBGE).
    /// </summary>
    /// <param name="state">Nome do estado</param>
    [HttpGet("/cities/state")]
    public async Task<IActionResult> GetCitiesByState([FromQuery] string state)
    {
      var result = await _getCitiesUseCase.GetCitiesByState(state);

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


    /// <summary>
    /// Retorna uma cidade pelo nome(IBGE).
    /// </summary>
    /// <param name="name">Nome da cidade no IBGE</param>
    [HttpGet("/cities/name")]
    public async Task<IActionResult> GetCityByName([FromQuery] string name)
    {
      var result = await _getCitiesUseCase.GetCityByName(name);

      if (!result.Result)
        return BadRequest(result.Message);

      return Ok(result.Data);
    }
  }
}