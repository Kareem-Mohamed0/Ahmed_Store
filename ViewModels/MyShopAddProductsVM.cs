using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class MyShopAddProductsVM
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public float? DiscountPercentage { get; set; }
        public string Description { get; set; }

        public float Price { get; set; }
        public int Stock { get; set; }
        public int ShopId { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection ProductImages { get; set; }
        public List<SelectListItem>? ProductCategoryList { get; set; }
    }
}
