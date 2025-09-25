using GeoBR.Application.DTOs;

namespace GeoBR.Application.UseCases
{
  public interface IGetCitiesUseCase
  {
    public Task<ServiceResult<List<CityReadDto>>> GetCities(CityFilterDto city);
    public Task<ServiceResult<CityReadDto>> GetCitiesById(int id);
  }
}