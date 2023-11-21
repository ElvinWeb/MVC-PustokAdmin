using System.ComponentModel.DataAnnotations;

namespace MVC.PracticeTask_1.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Description { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
