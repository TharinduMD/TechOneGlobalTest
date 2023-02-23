using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net;
using System.Text.Json.Nodes;
using Weather.Core;
using Weather.Core.Dto;
using Weather.Data;
using Weather.Service.IServices;

namespace Weather.Service
{
    public class CityService : ICityService
    {
        public readonly ILogger<CityService> _logger;
        public readonly Context _context;
        public readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CityService(ILogger<CityService> logger, Context context, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public object Result { get; set; }
        public string Message { get; set; }

        public IEnumerable<City> GetAllCities()
        {
            try
            {
                _logger.LogInformation("Getting All Cities");
                var cityList = _context.Cities.AsNoTracking().ToList();
                _logger.LogInformation("All Cities are fetched");
                return cityList;

            }
            catch (Exception ex)
            {
                _logger.LogError("Cities not fetchd : {ex}", ex);
                throw;
            }
        }
        public City GetCityById(int id)
        {
            try
            {
                _logger.LogInformation("Getting City by id");
                var city = _context.Cities.AsNoTracking().FirstOrDefault(x => x.CityId == id);
                if(city == null)
                {
                    _logger.LogInformation("cities not found");
                    return null;
                }
                _logger.LogInformation("city is fetched");
                return city;

            }
            catch (Exception ex)
            {
                _logger.LogError("Cities not fetchd : {ex}", ex);
                throw;
            }
        }
        public bool SaveCity(CityDto cityDto)
        {
            var success = false;
            try
            {
                _logger.LogInformation("Getting existing cities");
                var city = _mapper.Map<City>(cityDto);
                var cityExists = _context.Cities.Where(x => x.CityName == city.CityName).AsNoTracking().ToList();
                if(!cityExists.Any())
                {
                    _logger.LogInformation("existing cities not found");
                    _logger.LogInformation("Saving city");
                    city.CreatedBy = 1;
                    city.CreatedDate = DateTime.Now;
                    _context.Cities.Add(city);
                    _context.SaveChanges();
                    Result = city.CityId;
                    success = true;
                    _logger.LogInformation("city saved");

                }
                else {
                    Message = "existing cities found";
                    _logger.LogInformation("existing cities found");
                }
                return success;

            }
            catch (Exception ex)
            {
                Message = "Error occured while saving";
                _logger.LogError("Error occured while saving : {ex}", ex);
                throw;
            }
        }
        public bool UpdateCity(int id,CityDto cityDto)
        {
            var success = false;
            try
            {
                if(id == 0)
                {
                    _logger.LogInformation("city id = 0");
                    return success;
                }
                var city = _mapper.Map<City>(cityDto);
                _logger.LogInformation("Getting existing cities");
                var cityExists = _context.Cities.AsNoTracking().FirstOrDefault(x => x.CityId == id);
                if (cityExists != null)
                {
                    _logger.LogInformation("existing cities found");
                    _logger.LogInformation("Updating city");
                    cityExists.CityId = city.CityId;
                    cityExists.CityName = city.CityName;
                    cityExists.Latitude = city.Latitude;
                    cityExists.Longitude = city.Longitude;
                    cityExists.UpdatedBy = 1;
                    cityExists.UpdatedDate = DateTime.Now;
                    _context.Cities.Update(cityExists);
                    _context.SaveChanges();
                    Result = cityExists.CityId;
                    success = true;
                    _logger.LogInformation("city updated");
                }
                else
                {
                    Message = "Existing cities found";
                    _logger.LogInformation("existing cities found");
                }
                return success;

            }
            catch (Exception ex)
            {
                Message = "Error occured while updating";
                _logger.LogError("Error occured while updating : {ex}", ex);
                throw;
            }
        }
        public bool DeleteCity(int id)
        {
            var success = false;
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("city id = 0");
                    return success;
                }
                _logger.LogInformation("Getting existing cities");
                var cityExists = _context.Cities.AsNoTracking().FirstOrDefault(x => x.CityId == id);
                if (cityExists != null)
                {
                    _logger.LogInformation("existing cities found");
                    _logger.LogInformation("Deleting city");
                    cityExists.Deleted = true;
                    cityExists.DeletedBy = 1;
                    cityExists.DeletedDate = DateTime.Now;
                    _context.Cities.Update(cityExists);
                    _context.SaveChanges();
                    Result=cityExists.CityId;
                    success = true;
                    _logger.LogInformation("city deleted");
                }
                else
                {
                    Message = "Existing cities found";
                    _logger.LogInformation("existing cities found");
                }
                return success;

            }
            catch (Exception ex)
            {
                Message = "Error occured while deleting";
                _logger.LogError("Error occured while deleting : {ex}", ex);
                throw;
            }
        }

        public async Task<object> GetWeaterDetails(int cityId)
        {
            try
            {
                _logger.LogInformation("Getting City by id");
                var city = _context.Cities.AsNoTracking().FirstOrDefault(x => x.CityId == cityId);
                if (city != null)
                {
                    var client = new HttpClient();
                    var query = new Dictionary<string, string>()
                    {
                        ["lat"] = city.Latitude.ToString(),
                        ["lon"] = city.Longitude.ToString(),
                        ["appid"] = _configuration.GetSection("WeatherApi:AppId").Value
                    };
                    var uri = QueryHelpers.AddQueryString(_configuration.GetSection("WeatherApi:Url").Value, query);
                    var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    request.Headers.Add("Accept", "application/json");
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Result = JsonConvert.DeserializeObject<ExpandoObject>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("City weather not fetchd : {ex}", ex);
                throw;
            }
            return Result;
        }
    }
}
