using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _DbContext;
        public AuthorController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Author> authors = _DbContext.Authors.ToList();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid) return View();

            if (_DbContext.Authors.Any(a => a.FullName.ToLower() == author.FullName.ToLower()))
            {
                ModelState.AddModelError("FullName", "Author has already created!");
                return View();
            }

            _DbContext.Authors.Add(author);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            Author author = _DbContext.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (!ModelState.IsValid) return View();

            Author existAuthor = _DbContext.Authors.FirstOrDefault(a => a.Id == author.Id);

            if (existAuthor == null) return NotFound();

            if (_DbContext.Authors.Any(a => a.Id != author.Id && a.FullName.ToLower() == author.FullName.ToLower()))
            {
                ModelState.AddModelError("FullName", "Author has already created!");
                return View();
            }

            existAuthor.FullName = author.FullName;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Author author = _DbContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();

            _DbContext.Authors.Remove(author);
            _DbContext.SaveChanges();

            return Ok();
        }


    }
}
