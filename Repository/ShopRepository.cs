using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace AhmedStore.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly AppDbContext context;

        public ShopRepository(AppDbContext Context)
        {
            context = Context;
        }

        public List<SelectListItem> CategoriesSelectListItem()
        {
            var ShopCategories = context.ShopCategories.Select(S => new SelectListItem
            {
                Text = S.Name,
                Value = S.Id.ToString()
            });
            return ShopCategories.ToList();
        }

        public void Delete(int id)
        {
            context.Shops.Remove(GetById(id));
            context.SaveChanges();
        }

        public Shop Details(int id)
        {
            var Shop = context.Shops.FirstOrDefault(s => s.Id == id);
            return Shop;

        }

        public void Edit(int id, Shop Newshop)
        {
            var currentShop = context.Shops.FirstOrDefault(c => c.Id == id);
            if (currentShop != null)
            {
                currentShop.ShopOwnerId = Newshop.ShopOwnerId;
            }
            context.Shops.Update(currentShop);
            context.SaveChanges();
        }

        public List<Shop> GetAll()
        {
            var shops = context.Shops.ToList();
            return shops;
        }

        public Shop GetById(int id)
        {
            var Shop = context.Shops.FirstOrDefault(c => c.Id == id);
            if (Shop != null)
            {
                return Shop;
            }
            throw new Exception("No Category Found");
        }

        public void Insert(Shop NewShop)
        {
            context.Shops.Add(NewShop);
            context.SaveChanges();
        }

        public List<SelectListItem> ShopListItem()
        {
            var shoplist = context.Shops.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return shoplist;
        }

        public List<Shop> ShopWithCategory()
        {
            var shopswithcategory = context.Shops.Include(s => s.ShopCategory).ToList();
            return shopswithcategory;
        }
        public List<Shop> ShopWithCategoryandOwner()
        {
            var shopswithcategoryAndOwner = context.Shops
                                            .Include(O => O.ShopOwner)
                                            .Include(C => C.ShopCategory)
                                            .ToList();
            return shopswithcategoryAndOwner;
        }

      
        
    }
}
