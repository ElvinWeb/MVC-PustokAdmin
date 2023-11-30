using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {
        private readonly AppDbContext _DbContext;
        public GenreController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Genre> genres = _DbContext.Genres.ToList();
            return View(genres);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid) return View();

            if (_DbContext.Genres.Any(g => g.Name.ToLower() == genre.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "genre has already created!");
                return View();
            }

            _DbContext.Genres.Add(genre);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            Genre genre = _DbContext.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            if (!ModelState.IsValid) return View();

            Genre existGenre = _DbContext.Genres.FirstOrDefault(g => g.Id == genre.Id);

            if (existGenre == null) return NotFound();

            if (_DbContext.Genres.Any(g => g.Id != genre.Id && g.Name.ToLower() == genre.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "genre has already created!");
                return View();
            }

            existGenre.Name = genre.Name;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Genre genre = _DbContext.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null) return NotFound();
            _DbContext.Genres.Remove(genre);
            _DbContext.SaveChanges();

            return Ok();
        }


    }
}
