using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedStore.ViewModels
{
    [NotMapped]
    public class ShopCategoryVM
    {
        public string Name { get; set; }
    }
}
