using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _DbContext;
        private readonly AppViewModel _ViewModel;
        public BookController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Book> Books = _DbContext.Books.ToList();

            return View(Books);
        }
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();

            if (!ModelState.IsValid) return View();

            if (!_DbContext.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author is not found!");
                return View();
            }

            if (!_DbContext.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre is not found!");
                return View();
            }

            bool check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_DbContext.Tags.Any(t => t.Id == tagId))
                    {
                        check = true;
                        break;
                    }

                }
            }

            if (check)
            {
                ModelState.AddModelError("TagId", "Tag id not found!");
                return View();
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag()
                        {
                            Book = book,
                            TagId = tagId,
                        };

                        _DbContext.BookTags.Add(bookTag);
                    }
                }
            }

            _DbContext.Books.Add(book);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();

            if (id == null) return NotFound("Error");

            Book book = _DbContext.Books.FirstOrDefault(b => b.Id == id);

            if (book == null) return NotFound("Error");


            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();

            Book wantedBook = _DbContext.Books.FirstOrDefault(b => b.Id == book.Id);

            if (wantedBook == null)
            {
                return NotFound("Error");
            }

            _DbContext.Books.Remove(wantedBook);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //Optional
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();

            if (id == null) return NotFound();

            Book book = _DbContext.Books.FirstOrDefault(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {

            ViewBag.Authors = _DbContext.Authors.ToList();
            ViewBag.Genres = _DbContext.Genres.ToList();
            ViewBag.Tags = _DbContext.Tags.ToList();

            if (!ModelState.IsValid) return View();

            Book existBook = _DbContext.Books.FirstOrDefault(b => b.Id == book.Id);

            if (existBook == null) return NotFound("Error");

            if (!_DbContext.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author is not found!");
                return View();
            }

            if (!_DbContext.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre is not found!");
                return View();
            }


            existBook.Name = book.Name;
            existBook.Desc = book.Desc;
            existBook.Tax = book.Tax;
            existBook.Code = book.Code;
            existBook.SalePrice = book.SalePrice;
            existBook.CostPrice = book.CostPrice;
            existBook.IsAvailable = book.IsAvailable;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
