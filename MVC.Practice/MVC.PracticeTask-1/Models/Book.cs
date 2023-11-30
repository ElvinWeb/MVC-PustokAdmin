using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.PracticeTask_1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Tax { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public bool isNew { get; set; }
        public bool isFeatured { get; set; }
        public bool isBestseller { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public List<BookTag>? BookTags { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        [NotMapped]
        public List<int>? BookImageIds { get; set; }
        [NotMapped]
        public IFormFile? BookMainImage { get; set; }
        [NotMapped]
        public IFormFile? BookHoverImage { get; set; }
        [NotMapped]
        public List<IFormFile>? ImageFiles { get; set; }

        public List<BookImage>? BookImages { get; set; }
    }
}
