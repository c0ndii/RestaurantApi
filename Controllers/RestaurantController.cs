﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using RestaurantApi.Services;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
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
            if(restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantservice.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }
    }
}