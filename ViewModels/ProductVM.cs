using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? DiscountPercentage { get; set; } // nullable if no discount
        public string Image {  get; set; }
        public string ShopName { get; set; }

        //Calculated discounted price

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
