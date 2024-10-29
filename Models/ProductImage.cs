using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
