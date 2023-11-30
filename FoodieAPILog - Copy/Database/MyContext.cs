using HungryHUB.Entity;
using Microsoft.EntityFrameworkCore;

namespace HungryHUB.Database
{
    public class MyContext : DbContext
    {
        private readonly IConfiguration configuration;

        public MyContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public DbSet<User>? Users { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<MenuItem> MenuItem { get; set; }

        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Wallet> Wallets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnection"));
        }

    }
}
