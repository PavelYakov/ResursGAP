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
        public DbSet<Route> Route { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Truck)
                .WithMany()
                .HasForeignKey(o => o.TruckId);

            //modelBuilder.Entity<Route>()
            //    .HasOne(r => r.Order)       // Устанавливаем связь с заказом
            //    .WithMany()    // Указываем, что у заказа может быть много маршрутов
            //    .HasForeignKey(r => r.OrderId);  // Внешний ключ в маршруте для заказа

            //modelBuilder.Entity<Route>()
            //    .HasOne(r => r.City)        // Устанавливаем связь с городом
            //    .WithMany()    // Указываем, что у города может быть много маршрутов
            //    .HasForeignKey(r => r.CityId);  // Внешний ключ в маршруте для города

            //modelBuilder.Entity<Route>()
            //    .HasOne(r => r.Truck)       // Устанавливаем связь с фурой
            //    .WithMany()    // Указываем, что у фуры может быть много маршрутов
            //    .HasForeignKey(r => r.TruckId); // Внешний ключ в маршруте для фуры

        }

    }
}
