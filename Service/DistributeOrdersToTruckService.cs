using Microsoft.EntityFrameworkCore;
using ResursGAP.Models;

namespace ResursGAP.Service
{
    public class DistributeOrdersToTruckService
    {
        private readonly AppDbContext _dbContext;
        public DistributeOrdersToTruckService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Сервис по подбору машины на основе веса груза
        public void DistributeOrdersToTrucks(List<Order> orders, List<Truck> trucks)
        {
            foreach (var order in orders)
            {
                // Находим подходящую фуру на основе веса заказа
                var suitableTruck = trucks.FirstOrDefault(t => t.WeightInTons >= order.Weight);

                if (suitableTruck != null)
                {
                    // Присваиваем заказу фуру
                    order.Truck = suitableTruck;
                    order.TruckId = suitableTruck.TruckId;
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Фура не найдена");
                }
            }
        }

    }
}
