using NuGet.Protocol.Plugins;
using ResursGAP.Models;

namespace ResursGAP.Service
{
    public class DeliveryService
    {
        private readonly AppDbContext _dbContext;

        public DeliveryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Сервис по расчету стоимости доставки
        public decimal CalculateDeliveryCost(string senderCity, string receiverCity)
        {
            var sender = _dbContext.Cities.FirstOrDefault(c => c.Name == senderCity);
            var receiver = _dbContext.Cities.FirstOrDefault(c => c.Name == receiverCity);


            if (sender == null || receiver == null)
            {
                
                throw new InvalidOperationException("Город не найден");
            }

            // Расчет стоимости на основе зоновых цен
            var cost = Math.Abs(sender.ZonePrice + receiver.ZonePrice); 
            return cost;
        }
    }
}
