using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; } 
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get;set; } 
        public virtual Shop Shop { get; set; }
    }
}
