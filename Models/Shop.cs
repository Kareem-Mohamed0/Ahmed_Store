using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShopImage {  get; set; }
        public int ShopCategoryId { get; set; }
        public string Description { get; set; }
        [ForeignKey("ShopOwner")]
        public string? ShopOwnerId { get; set; }
        public string ShopAddress { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public User ShopOwner { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ShopCategory ShopCategory { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

    }
}
