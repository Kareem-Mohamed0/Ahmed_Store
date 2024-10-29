using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    public class AddProductVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float? DiscountPercentage { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public int ShopId { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection ProductImages { get; set; }
        public List<SelectListItem>? ShopList { get; set; }
        public List<SelectListItem>? ProductCategoryList { get; set; }
    }
}
