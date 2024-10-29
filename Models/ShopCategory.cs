namespace AhmedStore.Models
{
    public class ShopCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
