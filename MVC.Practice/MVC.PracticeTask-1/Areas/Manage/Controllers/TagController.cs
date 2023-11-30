using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _DbContext;
        public TagController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _DbContext.Tags.ToList();

            return View(tags);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            if (_DbContext.Tags.Any(t => t.Name.ToLower() == tag.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "tag has already created!");
                return View();
            }

            _DbContext.Tags.Add(tag);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            Tag tag = _DbContext.Tags.FirstOrDefault(t => t.Id == id);

            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            Tag existTag = _DbContext.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existTag == null) return NotFound();

            if (_DbContext.Tags.Any(t => t.Id != tag.Id && t.Name.ToLower() == tag.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "tag has already created!");
                return View();
            }

            existTag.Name = tag.Name;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Tag tag = _DbContext.Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();

            _DbContext.Tags.Remove(tag);
            _DbContext.SaveChanges();

            return Ok();
        }


    }
}
