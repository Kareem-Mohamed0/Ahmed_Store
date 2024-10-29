using AhmedStore.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    public class AddShopVM
    {
        
        public string Name { get; set; }
        public IFormFile ShopImage { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int ShopCategoryId { get; set; }
        public List<SelectListItem>? ShopCategories { get; set; }
    }
}
