using Microsoft.AspNetCore.Identity;

namespace TabMakerApp.Models
{
    // ApplicationUser class inherits from Identity and adds a field used to define one-to-many relationship with Tabs
    public class ApplicationUser : IdentityUser
    {
        // initialise HashSet so that it is not null
        public ApplicationUser()
        {
            this.Tabs = new HashSet<Tab>();
        }
        public virtual ICollection<Tab> Tabs { get; set; }
    }
}
