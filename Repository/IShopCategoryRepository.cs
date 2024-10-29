using AhmedStore.Models;

namespace AhmedStore.Repository
{
    public interface IShopCategoryRepository
    {
        ShopCategory GetById(int id);
        List<ShopCategory> GetAll();
        void Insert(ShopCategory NewCategory);
        void Edit(int id, ShopCategory NewCategory);
        void Delete(int id);

    }
}
