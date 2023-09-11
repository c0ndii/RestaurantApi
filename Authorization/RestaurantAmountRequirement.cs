using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public class RestaurantAmountRequirement : IAuthorizationRequirement
    {
        public int _restaurantAmount { get; }
        public RestaurantAmountRequirement(int restaurantAmount)
        {

            _restaurantAmount = restaurantAmount;
        }
    }
}
