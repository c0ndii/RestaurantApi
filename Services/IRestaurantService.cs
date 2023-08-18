﻿using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Update(ModifyRestaurantDto modifyRestaurantDto, int id);
    }
}