namespace AhmedStore.ViewModels
{
    public class MyShopProductsVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string ProductCategory { get; set; }
        public float? Discount  { get; set; }
    }
}
