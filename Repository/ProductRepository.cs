using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AhmedStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext Context)
        {
            context = Context;
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void Edit(int id, Product NewProduct)
        {
            var CurrentProduct = GetById(id);
            if (CurrentProduct != null)
            {
                CurrentProduct.ProductName = NewProduct.ProductName;
                CurrentProduct.Description = NewProduct.Description;
                CurrentProduct.Price = NewProduct.Price;
                CurrentProduct.Stock = NewProduct.Stock;
                CurrentProduct.DiscountPercentage = NewProduct.DiscountPercentage;
                CurrentProduct.CategoryId = NewProduct.CategoryId;
                CurrentProduct.ShopId = NewProduct.ShopId;

                context.SaveChanges();
            }
            else throw new Exception(message: "The OldProduct Not Found!");
        }

        public List<Product> GetAll()
        {
            var products = context.Products.AsNoTracking();
            return products.ToList();
        }

        public Product GetById(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return product;
            else throw new Exception(message:"Not Found");
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public List<ProductVM> AllProducstWithOneImage()
        {
            var products = context.Products.Include(i => i.ProductImages).Include(s => s.Shop).Select(i => new ProductVM
            {
                Id = i.Id,
                Name = i.ProductName,
                Price = i.Price,
                DiscountPercentage = i.DiscountPercentage,
                Image = i.ProductImages.FirstOrDefault().ImageName,
                ShopName = i.Shop.Name
            }).AsNoTracking();
            return products.ToList();
        }

        public List<SelectListItem> ShopList()
        {
            var shops = context.Shops.Select(S => new SelectListItem
            {
                Text = S.Name,
                Value = S.Id.ToString()
            });
            return shops.ToList();
        }

        public List<SelectListItem> ProductCategoryList()
        {
            var ProductCategoriesList = context.Categories.Select(S => new SelectListItem
            {
                Text = S.Name,
                Value = S.Id.ToString()
            });
            return ProductCategoriesList.ToList();
        }

        public void AddImagesToProduct(int ProductId, string[] Images)
        {
            for (int i =0; i < Images.Length; i++)
            {
                var ProductImage = new ProductImage()
                {
                    ImageName = Images[i],
                    ProductId = ProductId
                };
                context.Add(ProductImage);
                context.SaveChanges();
            }
        }

        public List<AllProductsForAdminVM> AllProductsForAdmin()
        {
            var Products = context.Products.Include(P => P.Category)
                .Include(P => P.Shop).Select(P => new AllProductsForAdminVM
                {
                    Id = P.Id,
                    ProductName = P.ProductName,
                    Price = P.Price,
                    ProductCategory = P.Category.Name,
                    ShopName = P.Shop.Name,
                    Stock = P.Stock
                });
            return Products.ToList();
        }
        /*----------------------- DisplayProductsForUsers ---------------------------*/


    }
}
