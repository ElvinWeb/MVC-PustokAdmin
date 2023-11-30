using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.Helpers;
using System.Globalization;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _DbContext;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext _context, IWebHostEnvironment env)
        {
            _DbContext = _context;
            _env = env;
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
            string fileName = string.Empty;

            if (!ModelState.IsValid) return View(slide);
            if (slide.Image != null)
            {
                fileName = slide.Image.FileName;

                if (slide.Image.ContentType != "image/png" && slide.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", "please select correct file type");
                }

                if (slide.Image.Length > 1048576)
                {
                    ModelState.AddModelError("Image", "file size should be more lower than 1mb ");
                }

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
            }
            else
            {
                ModelState.AddModelError("Image", "Must be choosed!!");
                return View();
            }

            string folder = "uploads/bg-slide";
            string newFileName = Helper.GetFileName(_env.WebRootPath, folder, slide.Image);

            slide.ImgUrl = newFileName;

            _DbContext.Slides.Add(slide);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slide slide = _DbContext.Slides.FirstOrDefault(s => s.Id == id);
            if (slide == null) return NotFound();
            return View(slide);
        }

        [HttpPost]
        public IActionResult Update(Slide slide)
        {

            Slide wantedSlide = _DbContext.Slides.FirstOrDefault(s => s.Id == slide.Id);

            if (wantedSlide == null) return NotFound();

            if (!ModelState.IsValid) return View();

            string fileName = string.Empty;

            if (slide.Image != null)
            {
                fileName = slide.Image.FileName;

                if (slide.Image.ContentType != "image/png" && slide.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", "please select correct file type");
                    return View(slide);
                }

                if (slide.Image.Length > 1048576)
                {
                    ModelState.AddModelError("Image", "file size should be more lower than 1mb ");
                    return View(slide);
                }

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
                string folderPath = "uploads/bg-slide";
                string expiredFileName = Helper.GetFileName(_env.WebRootPath, folderPath, slide.Image);

                string wantedPath = Path.Combine(_env.WebRootPath, folderPath, wantedSlide.ImgUrl);

                if (System.IO.File.Exists(wantedPath))
                {
                    System.IO.File.Delete(wantedPath);
                }

                wantedSlide.ImgUrl = expiredFileName;
            }

            wantedSlide.Title = slide.Title;
            wantedSlide.Description = slide.Description;
            wantedSlide.BtnText = slide.BtnText;
            wantedSlide.RedirectUrl = slide.RedirectUrl;



            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Slide slide = _DbContext.Slides.FirstOrDefault(s => s.Id == id);

            if (slide == null) return NotFound();

            _DbContext.Slides.Remove(slide);
            _DbContext.SaveChanges();

            return Ok();
        }


    }
}
