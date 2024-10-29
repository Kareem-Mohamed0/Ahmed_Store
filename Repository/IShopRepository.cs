using AhmedStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.Repository
{
    public interface IShopRepository
    {
        Shop GetById(int id);
        List<Shop> GetAll();
        void Insert(Shop NewCategory);
        void Edit(int id, Shop NewCategory);
        Shop Details(int id);
        void Delete(int id);
        List<SelectListItem> CategoriesSelectListItem();
        List<Shop> ShopWithCategory();
        List<SelectListItem> ShopListItem();
        public List<Shop> ShopWithCategoryandOwner();
    }
}
