using AhmedStore.Models;
using AhmedStore.ViewModels;

namespace AhmedStore.Repository
{
    public interface ISearchRepository
    {
        List<Shop> ShopByCategory(string Cat);
        List<Shop> ShopByName(string ShopName, string ShopCategory);
        public List<ProductVM> DisplayProductsForUsers(int id);
        public List<ProductVM> SearchProductsByCategory(int CategoryId);
        public IEnumerable<Shop> GetAllShops();
        public IEnumerable<Product> GetAllProducts();
        public IEnumerable<Category> GetCategoriesOfShop(int ShopId);
        public Shop GetShopByName(string Name);
        public IEnumerable<ProductVM> ProductSearch(string Search, string ShopName);
        public Shop GetShopById(int id);

    }
}
