using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResursGAP.Models;
using ResursGAP.Service;
using Route = ResursGAP.Models.Route;

namespace ResursGAP.Controllers
{
    public class OrderController : Controller
    {
        private readonly DeliveryService _deliveryService;
        private readonly AppDbContext _dbContext;
        private readonly DistributeOrdersToTruckService _distributeService;
        public OrderController(AppDbContext dbContext, DeliveryService deliveryService, DistributeOrdersToTruckService distributeService)
        {
            _dbContext = dbContext;
            _deliveryService = deliveryService;
            _distributeService = distributeService;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            
         var orders = _dbContext.Orders
          .Include(o => o.Truck) // Включить связанный объект Truck
          .ToList();

            if (orders == null)
            {

                return NotFound();
            }
            var ordersViewModels = orders.Select(c => new Order
            {
                OrderId = c.OrderId,
                SenderCity = c.SenderCity,
                ReceiverCity = c.ReceiverCity
            }).ToList();


            return View(ordersViewModels);

            
        }

        // Выбор машины на по массе груза
        public IActionResult GetAvailableTrucks(double weight)
        {
            // Запросите доступные грузовики из базы данных на основе массы груза
            var Trucks = _dbContext.Trucks.Where(t => t.WeightInTons >= weight).ToList();

            // Отправьте список доступных грузовиков в частичное представление
            return PartialView("_PartialTruck", Trucks);
        }

        public ActionResult Details(int id)
        {
            var order = _dbContext.Orders.Find(id);

          
            if (order == null)
            {
                return NotFound(); 
            }

            
            return View(order);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            
            List<City> cities = _dbContext.Cities.ToList();
         

            List<SelectListItem> cityList = cities.Select(city => new SelectListItem
            {
                Text = city.Name
            }).ToList();
           
            ViewBag.CityList = cityList;

           

            return View();
        }

       
        // POST: OrderController/Create
        [HttpPost]
       
        public IActionResult Create(Order order)
        {
           
            decimal deliveryCost = _deliveryService.CalculateDeliveryCost(order.SenderCity, order.ReceiverCity);
            order.DeliveryCost = deliveryCost;
            double weight = order.Weight;

            decimal newDeliveryCost = deliveryCost * (decimal)order.Weight; 
            order.DeliveryCost = newDeliveryCost;
           
            // Найдите подходящий грузовик на основе массы груза
            Truck selectedTruck = _dbContext.Trucks.FirstOrDefault(truck => truck.WeightInTons >= weight && truck.LoadedWeightInTons + weight <= truck.WeightInTons);

            

            if (selectedTruck != null)
            {
                // Если найден подходящий грузовик, установите TruckId
                order.TruckId = selectedTruck.TruckId;
                selectedTruck.LoadedWeightInTons += weight;
                _dbContext.SaveChanges();
            }
            else
            {
             
               
               ModelState.AddModelError(string.Empty, "Нет подходящего грузовика для указанной массы груза.");
                return View(order);
            }

            if (ModelState.IsValid)
            {
                
                _dbContext.Orders.Add(order);
                 _dbContext.SaveChanges();

                return RedirectToAction("Index");
              

            }
           

            return RedirectToAction("Create");
            
        }

       public IActionResult RouteDetails(int truckId)
        {
            // Получить все заказы, связанные с определенной машиной (truckId)
            var ordersForTruck = _dbContext.Orders
                .Where(o => o.TruckId == truckId)
                .ToList();

            
            if (ordersForTruck.Count == 0)
            {
                ViewBag.Message = "Нет данных о заказах для этой машины.";
                return View();
            }

            // Загрузить информацию о машине 
            var truck = _dbContext.Trucks.FirstOrDefault(t => t.TruckId == truckId);

            if (truck == null)
            {
                
                return NotFound();
            }

            // Модель представления  ViewModel, которая  содержит информацию о машине и связанных заказах
            var viewModel = new RouteDetailsViewModel
            {
                Truck = truck,
                Orders = ordersForTruck
            };

            return View(viewModel);
        }

        [HttpGet]
      
        public IActionResult Delete(int orderId)
        {
            
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
          
            
            if (order == null)
            {
                return NotFound();
            }

            var truck = _dbContext.Trucks.FirstOrDefault(t => t.TruckId == order.TruckId);

            if (truck != null)
            {
                
                truck.LoadedWeightInTons = truck.LoadedWeightInTons-order.Weight;
            }

            // Удалите заказ из базы данных
            _dbContext.Orders.Remove(order);
           
            _dbContext.SaveChanges();

            
            return RedirectToAction("Index");
        }

    }
}
