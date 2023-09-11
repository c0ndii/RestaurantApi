using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using System.Security.Claims;

namespace RestaurantApi.Authorization
{
    public class RestaurantAmountRequirementHandler : AuthorizationHandler<RestaurantAmountRequirement>
    {
        private readonly ILogger<RestaurantAmountRequirementHandler> _logger;
        private readonly RestaurantDbContext _context;
        public RestaurantAmountRequirementHandler(ILogger<RestaurantAmountRequirementHandler> logger, RestaurantDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantAmountRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userRestaurantsCount = _context.Restaurants.Where(x => x.CreatedById == userId).ToList().Count;
            var userEmail = context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            _logger.LogInformation($"User {userEmail} with [{userRestaurantsCount}]");
            if(userRestaurantsCount >= requirement._restaurantAmount)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            } else
            {
                _logger.LogInformation("Authorization failed");
            }
            return Task.CompletedTask;
        }
    }
}
