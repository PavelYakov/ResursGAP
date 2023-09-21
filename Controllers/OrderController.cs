﻿using Microsoft.AspNetCore.Http;
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
            

            return View();
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
            

            if (ModelState.IsValid)
            {
                // Рассчитываем стоимость доставки
                decimal deliveryCost = _deliveryService.CalculateDeliveryCost(order.SenderCity, order.ReceiverCity);
                order.DeliveryCost = deliveryCost;

                
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
           }

           //  Если модель недействительна, возвращаемся на страницу создания заказа
            return View(order);
        }

       
    }
}