using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResursGAP.Models;
using ResursGAP.Service;

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
            //var orders = _dbContext.Orders.ToList();
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            decimal deliveryCost = _deliveryService.CalculateDeliveryCost(order.SenderCity, order.ReceiverCity);
            order.DeliveryCost = deliveryCost;

            if (ModelState.IsValid)
            {
                // Рассчитываем стоимость доставки
                //decimal deliveryCost = _deliveryService.CalculateDeliveryCost(order.SenderCity, order.ReceiverCity);
                //order.DeliveryCost = deliveryCost;

                
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
           }
           return RedirectToAction("Index");
            //  Если модель недействительна, возвращаемся на страницу создания заказа
            //return View(order);
        }

       
    }
}
