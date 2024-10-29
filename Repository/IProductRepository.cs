using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.ObjectPool;

namespace AhmedStore.Repository
{
    public interface IProductRepository
    {
        List<ProductVM> AllProducstWithOneImage();
        List<SelectListItem> ShopList();
        List<SelectListItem> ProductCategoryList();
        List<AllProductsForAdminVM> AllProductsForAdmin();
        void AddImagesToProduct(int ProductId, string[] Images);
        Product GetById(int id);
        List<Product> GetAll();
        void Insert(Product product);
        void Edit(int id , Product product);
        void Delete(int id);

    }
}
