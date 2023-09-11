using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System.Security.Claims;

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
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
                throw new NotFoundException("Restaurant not found");
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
            var userId = _userContextService.GetUserId;
            restaurantdto.CreatedById = userId;
            _context.Add(restaurantdto);
            _context.SaveChanges();
            return restaurantdto.Id;
        }
        public void Delete(int id)
        {
            _logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var user = _userContextService.User;
            var authorizationResult = _authorizationService.AuthorizeAsync(user, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Forbidden");
            }
            _context.Remove(restaurant);
            _context.SaveChanges();
        }
        public void Update(ModifyRestaurantDto modifyRestaurantDto, int id)
        {
            var restaurantToBeUpdated = _context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurantToBeUpdated is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var user = _userContextService.User;
            var authorizationResult = _authorizationService.AuthorizeAsync(user, restaurantToBeUpdated, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Forbidden");
            }
            restaurantToBeUpdated.Name = modifyRestaurantDto.Name;
            restaurantToBeUpdated.Description = modifyRestaurantDto.Description;
            restaurantToBeUpdated.HasDelivery = modifyRestaurantDto.HasDelivery;
            _context.SaveChanges();
        }
    }
}
