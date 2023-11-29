using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
    }
}
