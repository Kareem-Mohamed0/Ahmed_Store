using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AhmedStore.Models
{
    public class User : IdentityUser
    {
        public string Image { get; set; }
        public DateOnly BirthDate { get; set; } 
        public string Address { get; set; }
        public Roles? Role { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Order> Orders { get;set; }
        public virtual Cart Cart { get; set; }

    }
}
