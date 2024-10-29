using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    public class AssignShopVM
    {
        public int ShopId { get; set; }
        public string ShopOwnerId { get; set; }
        public List<SelectListItem>? ShopList { get; set; }
    }
}
