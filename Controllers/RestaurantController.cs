﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantservice;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantservice = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _restaurantservice.GetAll();
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetOne([FromRoute]int id)
        {
            var restaurant = _restaurantservice.GetById(id);
            return Ok(restaurant);
        }
        [HttpPost]
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
