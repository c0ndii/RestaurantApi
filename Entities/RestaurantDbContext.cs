
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _ConnectionString = 
            "Data Source=(localdb)\\MSSQLLocalDB;Database=RestaurantDB;Trusted_Connection=True";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<Restaurant>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(x=>x.Name)
                .IsRequired();
            modelBuilder.Entity<Address>()
                .Property(x => x.City)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Address>()
                .Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(50);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_ConnectionString);
        }

    }
}
