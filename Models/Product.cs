using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }
        public virtual Shop Shop { get; set; }


        public float? DiscountPercentage { get; set; } // nullable if no discount

        // Calculated discounted price
        public float DiscountedPrice
        {
            get
            {
                return DiscountPercentage.HasValue
                    ? Price - (Price * (DiscountPercentage.Value / 100))
                    : Price;
            }
        }
    }
}
