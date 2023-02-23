using Weather.Core;
using Weather.Core.Dto;

namespace Weather.Service.IServices
{
    public interface ICityService
    {
        object Result { get; set; }
        string Message { get; set; }
        IEnumerable<City> GetAllCities();
        City GetCityById(int id);
        bool SaveCity(CityDto cityDto);
        bool UpdateCity(int id, CityDto cityDto);
        bool DeleteCity(int id);
        Task<object> GetWeaterDetails(int cityId);
    }
}
