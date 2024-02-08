using System.ComponentModel.DataAnnotations;

namespace TabMakerApp.Models
{
    public class Tab
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? TabContent { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
