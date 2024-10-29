using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
