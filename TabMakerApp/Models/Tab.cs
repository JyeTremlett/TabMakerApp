using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabMakerApp.Models
{
    public class Tab
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        [Required]
        [DisplayName("Tab Name")]
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? TabContent { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
