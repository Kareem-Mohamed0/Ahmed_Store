using AhmedStore.Data;
using AhmedStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext context;

        public ProductCategoryRepository(AppDbContext Context)
        {
            context = Context;
        }
        public void Delete(int id)
        {
            context.Categories.Remove(GetById(id));
            context.SaveChanges();
        }

        public void Edit(int id, Category NewCategory)
        {
            if (NewCategory != null)
            {
                var currentCategory = context.Categories.FirstOrDefault(c => c.Id == id);
                if (currentCategory != null)
                {
                    currentCategory.Name = NewCategory.Name;
                }
                context.SaveChanges();
            }
            throw new Exception(message: "Null New Category!");
        }

        public List<Category> GetAll()
        {
            var categories = context.Categories.ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                return category;
            }
            throw new Exception("No Category Found");
        }

        public void Insert(Category NewCategory)
        {
            context.Categories.Add(NewCategory);
            context.SaveChanges();
        }

        public List<SelectListItem> ShopList()
        {
            var list = context.Shops.Select(S => new SelectListItem
            {
                Text = S.Name,
                Value = S.Id.ToString()
            });
            return list.ToList();
        }
    }
}
