using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slide> Slides { get; set; }
    }
}
