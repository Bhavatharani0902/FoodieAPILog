using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;

        public CityController(ICityService cityService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            _cityService = cityService;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet, Route("GetAllCities")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            try
            {
                List<City> cities = _cityService.GetAllCities();
                List<CityDTO> citiesDto = _mapper.Map<List<CityDTO>>(cities);
                _logger.Info("Retrieved all cities successfully.");
                return StatusCode(200, citiesDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting all cities: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("AddCity")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] CityDTO cityDto)
        {
            try
            {
                City city = _mapper.Map<City>(cityDto);
                _cityService.CreateCity(city);
                _logger.Info($"City added successfully. City ID: {city.CityID}");
                return StatusCode(200, city);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error adding city: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditCity")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCity(CityDTO cityDto)
        {
            try
            {
                City city = _mapper.Map<City>(cityDto);
                _cityService.EditCity(city);
                _logger.Info($"City updated successfully. City ID: {city.CityID}");
                return StatusCode(200, city);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error updating city: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteCity/{cityID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCity(int cityID)
        {
            try
            {
                _cityService.DeleteCity(cityID);
                _logger.Info($"City deleted successfully. City ID: {cityID}");
                return StatusCode(200, new JsonResult($"City with ID {cityID} is deleted."));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error deleting city: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
