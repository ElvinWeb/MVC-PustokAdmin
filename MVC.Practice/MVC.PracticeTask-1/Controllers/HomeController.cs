using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _DbContext;
        private AppViewModel _ViewModel = new AppViewModel();
        public HomeController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {


            _ViewModel.Slides = _DbContext.Slides.ToList();
            _ViewModel.Services = _DbContext.Services.ToList();
            _ViewModel.NewBooks = _DbContext.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isNew).ToList();
            _ViewModel.FeaturedBooks = _DbContext.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isFeatured).ToList();
            _ViewModel.BestsellerBooks = _DbContext.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isBestseller).ToList();


            return View(_ViewModel);
        }
    }
}
