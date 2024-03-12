using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabMakerApp.Models
{
    public class Tab
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Tab Name")]
        public string? Name { get; set; }
        public string? Author { get; set; }
        [DisplayName("Tab Content")]
        public string? TabContent { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; } = null!;
    }
}
