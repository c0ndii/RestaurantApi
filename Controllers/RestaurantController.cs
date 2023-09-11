using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System.Security.Claims;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantservice;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantservice = restaurantService;
        }
        [HttpGet]
        [Authorize(Policy = "RestaurantCount")]
        public ActionResult<IEnumerable<Restaurant>> GetAll([FromQuery]RestaurantQuery query)
        {
            var restaurants = _restaurantservice.GetAll(query);
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Restaurant> GetOne([FromRoute]int id)
        {
            var restaurant = _restaurantservice.GetById(id);
            return Ok(restaurant);
        }
        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            var id = _restaurantservice.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantservice.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] ModifyRestaurantDto modifyRestaurantDto, [FromRoute] int id)
        {
            _restaurantservice.Update(modifyRestaurantDto, id);
            return Ok();
        }
    }
}
