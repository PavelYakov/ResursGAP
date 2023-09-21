using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResursGAP.Models;

namespace ResursGAP.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _context;
        public CityController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _context.Cities.ToList();

            if (cities == null || cities.Count == 0)
            {
                
                return NotFound();
            }
            var cityViewModels = cities.Select(c => new City
            {
                CityId = c.CityId,
                Name = c.Name,
                ZonePrice = c.ZonePrice
            }).ToList();

            
            return View(cityViewModels);
        }


       
        public ActionResult Details(int id)
        {
            
            var city = _context.Cities.Find(id);

            
            if (city == null)
            {
                return NotFound(); 
            }

            
            return View(city);
        }
    }
    
}
