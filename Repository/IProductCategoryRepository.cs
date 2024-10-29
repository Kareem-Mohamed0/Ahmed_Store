using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.Repository
{
    public interface IProductCategoryRepository
    {
        //        List<ProductVM> AllProducstWithOneImage();
        List<SelectListItem> ShopList();
        Category GetById(int id);
        List<Category> GetAll();
        void Insert(Category NewCategory);
        void Edit(int id, Category NewCategory);
        void Delete(int id);
    }
}
