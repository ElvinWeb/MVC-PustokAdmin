using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _DbContext;
        public SliderController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Slide> AllSlides = _DbContext.Slides.ToList();
            return View(AllSlides);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slide slide)
        {
            if (!ModelState.IsValid) return View();

            _DbContext.Slides.Add(slide);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slide slide = _DbContext.Slides.FirstOrDefault(x => x.Id == id);
            return View(slide);
        }

        [HttpPost]
        public IActionResult Update(Slide slide)
        {
            if (!ModelState.IsValid) return View();
            Slide existSlide = _DbContext.Slides.FirstOrDefault(x => x.Id == slide.Id);

            existSlide.Title = slide.Title;
            existSlide.Description = slide.Description;
            existSlide.ImgUrl = slide.ImgUrl;
            existSlide.RedirectUrl = slide.RedirectUrl;
            existSlide.BtnText = slide.BtnText;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Slide slide = _DbContext.Slides.FirstOrDefault(x => x.Id == id);
            return View(slide);
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {

            Slide existSlide = _DbContext.Slides.FirstOrDefault(x => x.Id == service.Id);

            if (existSlide == null)
            {
                return NotFound();
            }
            _DbContext.Slides.Remove(existSlide);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
