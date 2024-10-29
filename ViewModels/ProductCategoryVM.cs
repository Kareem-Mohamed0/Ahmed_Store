using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhmedStore.ViewModels
{
    public class ProductCategoryVM
    {
        [Required]
        public string Name { get; set; }
        public int ShopId { get; set; }
        public List<SelectListItem>? ShopList { get; set; }
    }
}
