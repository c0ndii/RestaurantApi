using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        IEnumerable<DishDto> GetAll(int restaurantId);
        DishDto GetById(int restaurantId, int dishId);
        void RemoveAll(int restaurantId);
        void RemoveById(int restaurantId, int dishId);
    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;
            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();
            return dishEntity.Id;
        }
        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId && x.RestaurantId == restaurantId);
            if (dish is null)
                throw new NotFoundException("Dish not found");
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishDtos;
        }
        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            _context.Dishes.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }
        public void RemoveById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _context
                .Dishes
                .FirstOrDefault(x => x.RestaurantId == restaurantId && x.Id == dishId);
            if (dish is null)
                throw new NotFoundException("Dish not found");
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context.Restaurants.Include(x => x.Dishes).FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            return restaurant;
        }
    }
}
