using AhmedStore.Migrations;
using AhmedStore.Models;

namespace AhmedStore.Repository
{
    public interface ICartRepository
    {
        bool CheckShopUnity(int ShopId, int CartId);
        Cart CreateCart(string UserId,int ShopId);
        void DeleteCart(int CartID);
        bool CartCheck(string UserId);
        void AddProductToCart(int CartId, int ProductId);
        Shop GetShopByProductId(int ProductId);
        public Cart GetCartByUserId(string UserId);
        public bool CartProductExistBefore(int ProductId, int CartId);
        public Cart GetCart(string UserId);
        public Cart GetCartProducts(int CartId);
        public void DeleteProductFromCart(int CartId,int ProductId);
        public Shop GetShopById(int ShopId);
        public void DeleteCartByUserName(string UserName);




    }
}
