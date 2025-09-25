using GeoBR.Application.DTOs;

namespace GeoBR.Application.UseCases
{
  public interface IGetCitiesUseCase
  {
    public Task<ServiceResult<List<CityReadDto>>> GetCities();
    public Task<ServiceResult<List<CityReadDto>>> GetCitiesByState(string state);
    public Task<ServiceResult<CityReadDto>> GetCitiesById(int id);
    public Task<ServiceResult<CityReadDto>> GetCityByName(string name);
  }
}