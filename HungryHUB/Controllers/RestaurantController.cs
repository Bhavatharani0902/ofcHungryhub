using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpGet]
       //[Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            try
            {
                var restaurants = _restaurantService.GetAllRestaurants();
                var restaurantDTOs = _mapper.Map<List<RestaurantDTO>>(restaurants);
                return StatusCode(200, restaurantDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{restaurantId}")]
        //[Authorize(Roles = "Admin, Restaurant")]
        public ActionResult<RestaurantDTO> GetRestaurantById(int restaurantId)
        {
            try
            {
                var restaurant = _restaurantService.GetRestaurantById(restaurantId);

                if (restaurant == null)
                {
                    return StatusCode(404); // Not Found
                }

                var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
                return StatusCode(200, restaurantDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult CreateRestaurant([FromBody] RestaurantDTO restaurantDTO)
        {
            try
            {
                var restaurant = _mapper.Map<Restaurant>(restaurantDTO);
                _restaurantService.CreateRestaurant(restaurant);
                return StatusCode(200); // 200 OK for success
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{restaurantId}")]
        //[Authorize(Roles = "Admin, Restaurant")]
        public ActionResult UpdateRestaurant(int restaurantId, [FromBody] RestaurantDTO restaurantDTO)
        {
            try
            {
                var existingRestaurant = _restaurantService.GetRestaurantById(restaurantId);

                if (existingRestaurant == null)
                {
                    return StatusCode(404); // Not Found
                }

                var updatedRestaurant = _mapper.Map<Restaurant>(restaurantDTO);
                _restaurantService.UpdateRestaurant(restaurantId, updatedRestaurant);

                return StatusCode(200); // 200 OK for success
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{restaurantId}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult DeleteRestaurant(int restaurantId)
        {
            var existingRestaurant = _restaurantService.GetRestaurantById(restaurantId);

            if (existingRestaurant == null)
            {
                return StatusCode(404); // Not Found
            }

            _restaurantService.DeleteRestaurant(restaurantId);

            return StatusCode(200); // 200 OK for success
        }
        [HttpGet("GetRestaurantsByCity")]
        public ActionResult<List<Restaurant>> GetRestaurantsByCity(string cityName)
        {
            try
            {
                var restaurants = _restaurantService.GetRestaurantsByCity(cityName);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("GetRestaurantByName")]
        public IActionResult GetRestaurantByName([FromQuery] string name)
        {
            var restaurant = _restaurantService.GetRestaurantByName(name);

            if (restaurant == null)
            {
                return NotFound(); // Return 404 if the restaurant is not found
            }

            return Ok(restaurant);
        }
        [HttpPut("{restaurantId}/Activate")]
        //[Authorize(Roles = "Admin")]
        public ActionResult ActivateRestaurant(int restaurantId)
        {
            try
            {
                _restaurantService.ActivateRestaurant(restaurantId);
                return StatusCode(200); // 200 OK for success
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{restaurantId}/Deactivate")]
        //[Authorize(Roles = "Admin")]
        public ActionResult DeactivateRestaurant(int restaurantId)
        {
            try
            {
                _restaurantService.DeactivateRestaurant(restaurantId);
                return StatusCode(200); // 200 OK for success
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
