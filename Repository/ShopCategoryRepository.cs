using AhmedStore.Data;
using AhmedStore.Models;

namespace AhmedStore.Repository
{
    public class ShopCategoryRepository : IShopCategoryRepository
    {
        private readonly AppDbContext context;

        public ShopCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            context.ShopCategories.Remove(GetById(id));
            context.SaveChanges();
        }

        public void Edit(int id, ShopCategory NewCategory)
        {
            if (NewCategory != null)
            {
                var currentCategory = context.ShopCategories.FirstOrDefault(c => c.Id == id);
                if (currentCategory != null)
                {
                    currentCategory.Name = NewCategory.Name;
                }
                context.SaveChanges();
            }
            throw new Exception(message: "Null New Category!");
        }

        public List<ShopCategory> GetAll()
        {
            var categories = context.ShopCategories.ToList();
            return categories;
        }

        public ShopCategory GetById(int id)
        {
            var category = context.ShopCategories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                return category;
            }
            throw new Exception("No Category Found");
        }

        public void Insert(ShopCategory NewCategory)
        {
            context.ShopCategories.Add(NewCategory);
            context.SaveChanges();
        }
    }
}
