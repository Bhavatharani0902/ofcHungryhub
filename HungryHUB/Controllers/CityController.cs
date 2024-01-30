using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HungryHUB.Entity;
using HungryHUB.DTO;
using HungryHUB.Services;
using log4net;
using System;
using System.Collections.Generic;

namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] 
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILog _logger;

        public CityController(ICityService cityService, ILog logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpPost, Route("CreateCity")]
        public IActionResult CreateCity(CityDTO cityDto)
        {
            try
            {
                City city = new City
                {
                    CityName = cityDto.CityName
                };

                _cityService.CreateCity(city);
                _logger.Info($"City created successfully. City ID: {city.CityID}");
                return StatusCode(201, "City created successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error creating city: {ex.Message}");
                return StatusCode(500, $"Error creating city: {ex.Message}");
            }
        }

        [HttpGet, Route("GetAllCities")]
        public IActionResult GetAllCities()
        {
            try
            {
                List<City> cities = _cityService.GetAllCities();
                _logger.Info("Retrieved all cities successfully.");
                return StatusCode(200, cities);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting all cities: {ex.Message}");
                return StatusCode(500, $"Error getting all cities: {ex.Message}");
            }
        }

        [HttpGet, Route("GetCityById/{id}")]
        public IActionResult GetCityById(int id)
        {
            try
            {
                City city = _cityService.GetCityById(id);
                if (city != null)
                {
                    _logger.Info($"Retrieved city by ID successfully. City ID: {city.CityID}");
                    return StatusCode(200, city);
                }
                else
                {
                    _logger.Warn($"City not found with ID: {id}");
                    return StatusCode(404, "City not found");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting city by ID: {ex.Message}");
                return StatusCode(500, $"Error getting city by ID: {ex.Message}");
            }
        }

        [HttpPut, Route("UpdateCity/{id}")]
        public IActionResult UpdateCity(int id, CityDTO updatedCityDto)
        {
            try
            {
                City existingCity = _cityService.GetCityById(id);

                if (existingCity != null)
                {
                    existingCity.CityName = updatedCityDto.CityName;
                    _cityService.UpdateCity(id, existingCity);
                    _logger.Info($"City updated successfully. City ID: {existingCity.CityID}");
                    return StatusCode(200, "City updated successfully.");
                }
                else
                {
                    _logger.Warn($"City not found with ID: {id}");
                    return StatusCode(404, "City not found");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error updating city: {ex.Message}");
                return StatusCode(500, $"Error updating city: {ex.Message}");
            }
        }

        [HttpDelete, Route("DeleteCity/{id}")]
        public IActionResult DeleteCity(int id)
        {
            try
            {
                _cityService.DeleteCity(id);
                _logger.Info($"City deleted successfully. City ID: {id}");
                return StatusCode(200, "City deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error deleting city: {ex.Message}");
                return StatusCode(500, $"Error deleting city: {ex.Message}");
            }
        }
    }
}
