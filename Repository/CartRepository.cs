using AhmedStore.Data;
using AhmedStore.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace AhmedStore.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;

        public CartRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddProductToCart(int CartId, int ProductId)
        {
            if (CartId != 0 && ProductId != 0)
            {
                var CartProduct = new CartProduct()
                {
                    CartId = CartId,
                    ProductId = ProductId
                };
                context.Add(CartProduct);
                context.SaveChanges();
            }

        }

        public bool CartCheck(string UserId)
        {
            if (UserId != null)
            {
                var result = context.Carts.Any(C => C.UserId == UserId);
                return result;
            }
            throw new Exception(message:"Can't Check the cart with null UserId");
        }

        public Cart CreateCart(string UserId, int shopId)
        {
            var cart = new Cart { UserId = UserId ,ShopId = shopId};
            context.Add(cart);
            context.SaveChanges();
            return cart;
        }

        public void DeleteCart(int CartId)
        {
            if (CartId != 0)
            {
                var cart = context.Carts.FirstOrDefault(c => c.Id == CartId);
                if (cart != null)
                {
                    context.Carts.Remove(cart);
                    context.SaveChanges();
                }
            }
            
        }

        public Shop GetShopByProductId(int ProductId)
        {
            if (ProductId != 0)
            {
                var shop = context.Shops
                    .FirstOrDefault(S => S.Products
                    .FirstOrDefault(P => P.Id == ProductId).Id == ProductId);
                return shop;
            }
            throw new Exception(message: "Can't get the shop of product");
        }
        public bool CartProductExistBefore(int ProductId,int CartId)
        {
            if(ProductId != 0)
            {
                var result = context.CartProducts
                    .Any(U => U.CartId == CartId && U.ProductId == ProductId); 
                return result;
            }
            throw new Exception(message: "Can't get cartproduct that has a zero in productId");
        }
        public Cart GetCartByUserId(string UserId)
        {
            var cart = context.Carts
                .Include(C => C.CartProducts)
                .FirstOrDefault(C => C.UserId == UserId);
                return cart;
        }
        /***********************************************/
        public Cart GetCart(string UserId)
        {
            var Cart = context.Carts
                .Include(C => C.CartProducts)
                .ThenInclude(P => P.Product)
                .ThenInclude(P => P.ProductImages)
                .FirstOrDefault(U => U.UserId == UserId);
            return Cart;
        }
        /*========================= Cart Products ========================*/
        public Cart GetCartProducts(int CartId)
        {
            var CartProducts = context.Carts
                .Include(C => C.CartProducts)
                .ThenInclude(C => C.Product)
                .ThenInclude(P => P.ProductImages)
                .FirstOrDefault(C => C.Id == CartId);
            return CartProducts;
        }

        /*========================= Delete Product From Cart ========================*/
        public void DeleteProductFromCart(int CartId, int ProductId)
        {
            var ProductInCart = context.CartProducts
                .FirstOrDefault(CP => CP.CartId == CartId && CP.ProductId == ProductId);
            context.Remove(ProductInCart);
            context.SaveChanges();
        }

        public bool CheckShopUnity(int ShopId, int CartId)
        {
            // Retrieve the cart using the provided CartId
            var cart = context.Carts.FirstOrDefault(S => S.Id == CartId);
            if (cart == null)
            {
                throw new Exception("No cart with the specified CartId.");
            }

            // Retrieve the first product associated with the cart
            var firstProduct = context.Products.FirstOrDefault(P => P.CartProducts.Any(x => x.CartId == CartId));

            // Check if a product was found and compare its ShopId
            if (firstProduct != null)
            {
                return firstProduct.ShopId == ShopId;
            }

            // If no products are found, assume the shops do not match
            return false;
        }

        public Shop GetShopById(int ShopId)
        {
            var Shop = context.Shops.FirstOrDefault(S => S.Id.Equals(ShopId));
            if (Shop != null)
                return Shop;
            else throw new Exception(message: "can't get the shop");
        }

        public void DeleteCartByUserName(string UserName)
        {
            var UserCart = context.Carts.FirstOrDefault(C => C.User.UserName == UserName);
            if (UserCart != null)
            {
                context.Carts.Remove(UserCart);
                context.SaveChanges();
            }
        }
    }
}
