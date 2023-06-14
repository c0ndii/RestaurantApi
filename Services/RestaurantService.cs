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
        public RestaurantService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
