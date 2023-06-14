using RestaurantApi.Entities;

namespace RestaurantApi
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _context;
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    _context.SaveChanges();
                }
            }
        }
        public RestaurantSeeder(RestaurantDbContext context)
        {
            _context = context;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "FastFood",
                    Description = "Super jedzenie",
                    ContactEmail = "test@wp.pl",
                    ContactNumber = "431123321",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Burgir",
                            Price = 25.50M,
                            Description = "test"
                        },
                        new Dish(){
                            Name = "Sałatka",
                            Price = 10M,
                            Description = "test123"
                        }
                    },
                    Address = new Address()
                    {
                        City = "Białystok",
                        Street = "1000-lecia",
                        PostalCode = "15-345"
                    }
                }
            };
            return restaurants;
        }
    }
}
