using AhmedStore.Migrations;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.Repository
{
    public interface IMyShopRepository
    {
        public string ShopName(string OwnerName);
        List<MyShopProductsVM> GetProducts(string ShopOwnerName);
        void AddProduct(string OwnerName,MyShopAddProductsVM Product,
                                                string[] Images);
        List<Category> MyCategories(string ShopOwnerName);
        List<SelectListItem> ProductCategoryList(string OwnerName);
        public void AddMyCategory(string OwnerName, Category model);
        public void Delete(int id);
        public Product GetById(int id);
        public bool DeleteProductCategory(int id);
        public string ReturnShopImageName(string OwnerName);
        /*****/
        public List<Order> MyOrders(string OwnerId);
        public void MarkStatus(int id,string status);
    }
}
