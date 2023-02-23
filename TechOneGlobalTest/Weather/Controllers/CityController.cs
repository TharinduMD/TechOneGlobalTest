using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Weather.Core;
using Weather.Core.Dto;
using Weather.Service.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather.Controllers
{
    [Route("cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private ResponseObject response;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet("")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public ResponseObject Get()
        {
            try
            {
                var result = _cityService.GetAllCities();
                if (result.Any())
                {
                    response = new ResponseObject
                    {
                        Result = result,
                        StatusCode = (int)HttpStatusCode.OK,
                        Success = true
                    };
                }
                else
                {
                    response = new ResponseObject
                    {
                        Result = null,
                        StatusCode = (int)HttpStatusCode.NoContent,
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                response = new ResponseObject
                {
                    Result = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false
                };
            }
            return response;
        }

        [HttpGet("{cityId}")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public ResponseObject Get(int cityId)
        {
            try
            {
                var result = _cityService.GetCityById(cityId);
                if (result != null)
                {
                    response = new ResponseObject
                    {
                        Result = result,
                        StatusCode = (int)HttpStatusCode.OK,
                        Success = true
                    };
                }
                else
                {
                    response = new ResponseObject
                    {
                        Result = null,
                        StatusCode = (int)HttpStatusCode.NoContent,
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                response = new ResponseObject
                {
                    Result = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false
                };
            }
            return response;
        }

        [HttpPost("")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public ActionResult Post(CityDto cityDto)
        {
            try
            {
                var result = _cityService.SaveCity(cityDto);
                if (result)
                {
                    return Ok(_cityService.Result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(_cityService.Message);
            }
        }

        [HttpPut("{cityId}")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public ActionResult Put(int cityId, CityDto cityDto)
        {
            try
            {
                var result = _cityService.UpdateCity(cityId, cityDto);
                if (result)
                {
                    return Ok(_cityService.Result);
                }
                else
                {
                    return BadRequest(_cityService.Message);
                }
            }
            catch (Exception)
            {
                return BadRequest(_cityService.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public ActionResult Delete(int cityId)
        {
            try
            {
                var result = _cityService.DeleteCity(cityId);
                if (result)
                {
                    return Ok(_cityService.Result);
                }
                else
                {
                    return BadRequest(_cityService.Message);
                }
            }
            catch (Exception)
            {
                return BadRequest(_cityService.Message);
            }
        }

        [HttpGet("weather-details/{cityId}")]
        [SwaggerOperation(Tags = new[] { "City" })]
        public async Task<ResponseObject> GetWeatherDetails(int cityId)
        {
            try
            {
                var result = await _cityService.GetWeaterDetails(cityId);
                if (result != null)
                {
                    response = new ResponseObject
                    {
                        Result = result,
                        StatusCode = (int)HttpStatusCode.OK,
                        Success = true
                    };
                }
                else
                {
                    response = new ResponseObject
                    {
                        Result = null,
                        StatusCode = (int)HttpStatusCode.NoContent,
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                response = new ResponseObject
                {
                    Result = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false
                };
            }
            return response;
        }
    }
}
