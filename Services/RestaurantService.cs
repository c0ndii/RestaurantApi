using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _context
                .Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return null;
            }
            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurantdtos = _context
                .Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();
            var result = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantdtos);
            return result;
        }
        public int Create(CreateRestaurantDto dto)
        {
            var restaurantdto = _mapper.Map<Restaurant>(dto);
            _context.Add(restaurantdto);
            _context.SaveChanges();
            return restaurantdto.Id;
        }
        public bool Delete(int id)
        {
            _logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurant is null)
            {
                return false;
            }
            _context.Remove(restaurant);
            _context.SaveChanges();
            return true;
        }
        public bool Update(ModifyRestaurantDto modifyRestaurantDto, int id)
        {
            var restaurantToBeUpdated = _context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurantToBeUpdated is null)
            {
                return false;
            }
            restaurantToBeUpdated.Name = modifyRestaurantDto.Name;
            restaurantToBeUpdated.Description = modifyRestaurantDto.Description;
            restaurantToBeUpdated.HasDelivery = modifyRestaurantDto.HasDelivery;
            _context.SaveChanges();
            return true;
        }
    }
}
