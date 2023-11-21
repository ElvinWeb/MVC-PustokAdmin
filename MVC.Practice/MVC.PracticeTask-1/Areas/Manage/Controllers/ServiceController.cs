using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _DbContext;
        public ServiceController(AppDbContext _context) 
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Service> allServices = _DbContext.Services.ToList();   
            return View(allServices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid) return View();

            _DbContext.Services.Add(service);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service service = _DbContext.Services.FirstOrDefault(x => x.Id == id);

            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            if (!ModelState.IsValid) return View();
            Service existService = _DbContext.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null)
            {
                return NotFound();
            }

            existService.Title = service.Title;
            existService.Description = service.Description;
            existService.Icon = service.Icon;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Service service = _DbContext.Services.FirstOrDefault(x => x.Id == id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {
            
            Service existService = _DbContext.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null)
            {
                return NotFound();
            }
            _DbContext.Services.Remove(existService);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
