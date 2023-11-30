using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.ViewModel
{
    public class AppViewModel
    {
        public List<Slide> Slides { get; set; }
        public List<Service> Services { get; set; }
        public List<Book> Books { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> BestsellerBooks { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}
