using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
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
            string fileName = slide.Image.FileName;

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
            if (!ModelState.IsValid) return View(slide);

            fileName = Guid.NewGuid().ToString() + fileName;


            string path = $"C:\\Users\\II novbe\\Desktop\\all task\\MVC-PustokAdmin\\MVC.Practice\\MVC.PracticeTask-1\\wwwroot\\uploads\\bg-slide\\{fileName}";

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slide.Image.CopyTo(fileStream);
            }

            slide.ImgUrl = fileName;

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
            //if (!ModelState.IsValid) return View();

            Slide existSlide = _DbContext.Slides.FirstOrDefault(x => x.Id == slide.Id);



            existSlide.Title = slide.Title;
            existSlide.Description = slide.Description;
            existSlide.ImgUrl = slide.Image.FileName;
            existSlide.RedirectUrl = slide.RedirectUrl;
            existSlide.BtnText = slide.BtnText;
            existSlide.Image = slide.Image;


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
        public IActionResult Delete(Slide slide)
        {

            Slide existSlide = _DbContext.Slides.FirstOrDefault(x => x.Id == slide.Id);

            string fileName = existSlide.ImgUrl;
            string path = $"C:\\Users\\II novbe\\Desktop\\all task\\MVC-PustokAdmin\\MVC.Practice\\MVC.PracticeTask-1\\wwwroot\\uploads\\bg-slide\\{fileName}";

            if (existSlide.ImgUrl != null)
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _DbContext.Slides.Remove(existSlide);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
