using Microsoft.EntityFrameworkCore;

namespace ResursGAP.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //public AppDbContext()
        //{
        //    Database.EnsureCreated();
        //}
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        //public DbSet<Route> Routes { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Truck)
                .WithMany()
                .HasForeignKey(o => o.TruckId);
           
        }

    }
}
