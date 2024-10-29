using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository searchRepository;
        private readonly IProductRepository productRepository;
        

        public SearchController(ISearchRepository searchRepository,IProductRepository productRepository)
        {
            this.searchRepository = searchRepository;
            this.productRepository = productRepository;
        }
        public IActionResult ShopSearchByCategory(string shopcat)
        {
            ViewBag.ShopCategory = shopcat;
            var shops = searchRepository.ShopByCategory(shopcat);
            return View("ShopSearch", shops);
        }
        public IActionResult ShopSearch(string Search,string ShopCategory)
        {
            ViewBag.ShopCategory = ShopCategory;
            var shops = searchRepository.ShopByName(Search, ShopCategory);
            return View("ShopSearch",shops);
        }
        public IActionResult DisplayProductsForUsers(int id,string ShopName)
        {
            if (id != 0)
            {
                var Shop = searchRepository.GetShopById(id);
                ViewBag.ShopName = Shop.Name;
            }
            if (ShopName != null)
            {
                var Shop = searchRepository.GetShopByName(ShopName);
                id = Shop.Id;
                ViewBag.ShopName = ShopName;
            }

            ViewBag.Categories = searchRepository.GetCategoriesOfShop(id);
            var products = searchRepository.DisplayProductsForUsers(id);
            return View("ProductSearch",products);
        }
        public IActionResult SearchProductsByCategory(int CategoryId,string ShopName)
        {
            ViewBag.ShopName = ShopName;
            var Shop = searchRepository.GetShopByName(ShopName);
            ViewBag.Categories = searchRepository.GetCategoriesOfShop(Shop.Id);
            return View("ProductSearch",searchRepository.SearchProductsByCategory(CategoryId));
        }
        public IActionResult ProductSearch(string Search,string id)
        {
            ViewBag.ShopName = id;
            var Shop = searchRepository.GetShopByName(id);
            ViewBag.Categories = searchRepository.GetCategoriesOfShop(Shop.Id);
            var products =searchRepository.ProductSearch(Search,id);
            if (products != null && products.Any())
            {
                return View("ProductSearch", products);
            }
            return View("ProductSearch",new List<ProductVM>());

        }
    }
}
