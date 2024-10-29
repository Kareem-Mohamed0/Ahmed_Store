using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext context;

        public SearchRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Shop> ShopByCategory(string Cat)
        {
            var shops = context.Shops.Where(S => S.ShopCategory.Name == Cat);
            return shops.ToList();
        }

        public List<Shop> ShopByName(string ShopName, string ShopCategory)
        {
            var shops = context.Shops.Where(S => S.ShopCategory.Name == ShopCategory && S.Name.Contains(ShopName));
            return shops.ToList();
        }
        public List<ProductVM> DisplayProductsForUsers(int ShopId)
        {
            var p = context.Products.Where(p => p.Shop.Id == ShopId).Select(N => new ProductVM
            {
                Id = N.Id,
                DiscountPercentage = N.DiscountPercentage,
                Image = N.ProductImages.FirstOrDefault().ImageName,
                Name = N.ProductName,
                Price = N.Price,
                ShopName = N.Shop.Name
            });
            return p.ToList();
        }
        public List<ProductVM> SearchProductsByCategory(int CategoryId)
        {
            var products = context.Products.Where(I => I.CategoryId == CategoryId)
                .Select(N => new ProductVM
                {
                    Id = N.Id,
                    DiscountPercentage = N.DiscountPercentage,
                    Image = N.ProductImages.FirstOrDefault().ImageName,
                    Name = N.ProductName,
                    Price = N.Price,
                    ShopName = N.Shop.Name
                });
            return products.ToList();
        }
        public IEnumerable<Shop> GetAllShops()
        {
            var shops = context.Shops;
            return shops;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            var products = context.Products;
            return products;
        }
        public IEnumerable<Category> GetCategoriesOfShop(int ShopId)
        {
            var categories = context.Categories.Where(I => I.ShopId == ShopId);
            return categories;
        }
        public Shop GetShopByName(string Name)
        {
            return (context.Shops.SingleOrDefault(S => S.Name == Name));
        }
        public IEnumerable<ProductVM> ProductSearch(string Search, string ShopName)
        {
            var products = context.Products.Where(P => P.ProductName.Contains(Search)
            && P.Shop.Name == ShopName).Select(P => new ProductVM
            {
                DiscountPercentage = P.DiscountPercentage,
                Id = P.Id,
                Image = P.ProductImages.SingleOrDefault().ImageName,
                Price = P.Price,
                ShopName = P.Shop.Name,
                Name = P.ProductName
            }).ToList();
            return products;
        }

        public Shop GetShopById(int id)
        {
            var shop = context.Shops.SingleOrDefault(P => P.Id == id);
            return shop;
        }
    }
}
