namespace MVC.PracticeTask_1.Models
{
    public class BookTag
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
