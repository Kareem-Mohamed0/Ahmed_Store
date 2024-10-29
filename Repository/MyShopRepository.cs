using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json.Serialization.Metadata;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Repository
{
    public class MyShopRepository : IMyShopRepository
    {
        private readonly AppDbContext context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public MyShopRepository(AppDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        /*======================================================================*/
        public string ShopName(string OwnerName)
        {
            var UserId = context.Users.FirstOrDefault(U => U.UserName == OwnerName).Id;
            var shopId = context.Shops.FirstOrDefault(S => S.ShopOwnerId == UserId).Id;
            var ShopName = context.Shops.FirstOrDefault(s => s.Id == shopId).Name;
            return ShopName;
        }
        /*============================== ShopImage return =============================*/
        public string ReturnShopImageName(string OwnerName)
        {
            var UserId = context.Users.FirstOrDefault(U => U.UserName == OwnerName).Id;
            var shopId = context.Shops.FirstOrDefault(S => S.ShopOwnerId == UserId).Id;
            var imgName = context.Shops.FirstOrDefault(d => d.Id == shopId).ShopImage;
            return imgName;
        }

        public void AddProduct(string OwnerName
            , MyShopAddProductsVM Product, string[] Images)
        {
            if (OwnerName is not null)
            {
                var UserId = context.Users.FirstOrDefault(U => U.UserName == OwnerName).Id;
                var shopId = context.Shops.FirstOrDefault(S => S.ShopOwnerId == UserId).Id;
                var p = new Product()
                {
                    Description = Product.Description,
                    CategoryId = Product.CategoryId,
                    DiscountPercentage = Product.DiscountPercentage,
                    ProductName = Product.ProductName,
                    Price = Product.Price,
                    Stock = Product.Stock,
                    ShopId = shopId
                };
                context.Products.Add(p);
                context.SaveChanges();
                for (int i = 0; i < Images.Length; i++) {
                    var PImage = new ProductImage()
                    {
                        ImageName = Images[i],
                        ProductId = p.Id
                    };
                    context.ProductImages.Add(PImage);
                    context.SaveChanges();
                }
            }
        }

        public List<MyShopProductsVM> GetProducts(string ShopOwnerName)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == ShopOwnerName).Id;
            var ShopId = context.Shops.FirstOrDefault(s => s.ShopOwnerId == userId).Id;
            var products = context.Products.Include(P => P.Category)
                .Where(P => P.ShopId == ShopId)
                .Select(P => new MyShopProductsVM
                {
                    Id = P.Id,
                    Discount = P.DiscountPercentage,
                    Price = P.Price,
                    Stock = P.Stock,
                    ProductName = P.ProductName,
                    ProductCategory = P.Category.Name
                });
            return products.ToList();
        }
        public List<SelectListItem> ProductCategoryList(string OwnerName)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == OwnerName).Id;
            var ShopId = context.Shops.FirstOrDefault(s => s.ShopOwnerId == userId).Id;
            var cats = context.Categories
                .Where(i => i.Shop.Id == ShopId)
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value= l.Id.ToString()
                });
            return cats.ToList();
        }
        /*===========================================================*/
        public List<Category> MyCategories(string ShopOwnerName)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == ShopOwnerName).Id;
            var ShopId = context.Shops.FirstOrDefault(s => s.ShopOwnerId == userId).Id;
            var cats = context.Categories
                .Where(S => S.Shop.Id == ShopId)
                .Select(S => new Category
                {
                    Id = S.Id,
                    Name = S.Name
                }).ToList();
            return cats;
        }
        /*============================================================*/
        public void AddMyCategory(string OwnerName, Category model)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == OwnerName).Id;
            var ShopId = context.Shops.FirstOrDefault(s => s.ShopOwnerId == userId).Id;
            model.ShopId = ShopId;
            context.Categories.Add(model);
            context.SaveChanges();
        }
        /*======================== CRUD Operations ===============================*/
        public void Delete(int id)
        {
            var product = GetById(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }
        public Product GetById(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return product;
            else throw new Exception(message: "Not Found");
        }
        /*=================== Delete Product Category =================*/
        public bool DeleteProductCategory(int id)
        {
            try
            {
                var Cat = context.Categories.FirstOrDefault(c => c.Id == id);
                if (Cat != null)
                {
                    context.Categories.Remove(Cat);
                    context.SaveChanges();
                    return true;
                }
                else throw new Exception($"Could not delete category that equal Null");
                

            }
            catch(Exception ex)
            {
                    return false;
            }
        }

        public List<Order> MyOrders(string OwnerId)
        {
            var ShopId = context.Shops.FirstOrDefault(S => S.ShopOwnerId == OwnerId).Id;
            var Orders = context.Orders.Where(O => O.ShopId == ShopId)
                .Include(O => O.OrderProducts)
                .ThenInclude(P => P.Product)
                .Include(U => U.User);
            return Orders.ToList();
        }

        public void MarkStatus(int id, string status)
        {
            var order = context.Orders.FirstOrDefault(O => O.Id == id);
            if (order != null)
            {
                order.Status = status;
            }
            context.SaveChanges();
        }
    }
}
