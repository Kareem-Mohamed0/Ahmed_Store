using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public float TotalAmount { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public User User { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
