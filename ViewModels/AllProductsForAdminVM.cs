using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    public class AllProductsForAdminVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string ShopName { get; set; }
        public string ProductCategory { get; set; }
    }
}
