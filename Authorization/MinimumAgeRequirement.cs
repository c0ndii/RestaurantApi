using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int minimumAge { get; }
        public MinimumAgeRequirement(int _minimumAge)
        {
            minimumAge = _minimumAge;
        }
    }
}
