using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.PracticeTask_1.Models
{
    public class Slide
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string BtnText { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string RedirectUrl { get; set; }   
        
        [StringLength(maximumLength: 100)]
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}
