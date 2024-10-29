using AhmedStore.Migrations;
using AhmedStore.Models;
using AhmedStore.Repository;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AhmedStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly ISearchRepository searchRepository;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public CartController(ICartRepository cartRepository,ISearchRepository searchRepository
            ,UserManager<User> userManager)
        {
            this.cartRepository = cartRepository;
            this.searchRepository = searchRepository;
            this.userManager = userManager;
        }

        [HttpPost]

        public async Task<IActionResult> AddProductToCart(string UserName, int ProductId)
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { redirectToLogin = true });
            }

            // Validate ProductId
            if (ProductId == 0)
            {
                return Json(new { success = false, message = "Invalid Product ID" });
            }

            // Find the user by username
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            // Get the shop for the provided product
            var shop = cartRepository.GetShopByProductId(ProductId);
            if (shop == null)
            {
                return Json(new { success = false, message = "Shop not found for the provided product" });
            }

            // Get or create the user's cart
            var cart = cartRepository.GetCartByUserId(user.Id) ?? cartRepository.CreateCart(user.Id, shop.Id);

            // Check for shop conflict
            if (cart.CartProducts != null && cart.CartProducts.Any())
            {
                var shopMatches = cartRepository.CheckShopUnity(shop.Id, cart.Id);
                if (!shopMatches)
                {
                    return Json(new
                    {
                        success = false,
                        conflict = true,  // Indicates conflict, prompting user action
                        message = "You have items from another shop in your cart. Do you want to delete them and proceed?"
                    });
                }
            }

            // Check if the product is already in the cart
            var productExists = cartRepository.CartProductExistBefore(ProductId, cart.Id);
            if (productExists)
            {
                return Json(new { success = true, message = "Product is already in your cart!" });
            }

            // Add the product to the cart
            cartRepository.AddProductToCart(cart.Id, ProductId);

            return Json(new { success = true, message = "Product added to cart successfully!" });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ClearCartAndAddProduct(string UserName, int ProductId)
        {
            if (ProductId == 0)
            {
                return Json(new { success = false, message = "Invalid Product ID" });
            }

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            var shop = cartRepository.GetShopByProductId(ProductId);
            if (shop == null)
            {
                return Json(new { success = false, message = "Shop not found for the provided product" });
            }

            // Clear the user's cart
            cartRepository.DeleteCartByUserName(UserName);

            // Create a new cart and add the product
            cartRepository.CreateCart(user.Id, shop.Id);
            var cart = cartRepository.GetCartByUserId(user.Id);
            cartRepository.AddProductToCart(cart.Id, ProductId);

            return Json(new { success = true, message = "Cart cleared and product added successfully!" });
        }

        /*------------------------------- Carts ------------------------------*/
        [Authorize]
        public IActionResult Carts()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result = cartRepository.GetCart(UserId);
            return View(Result);
        }
        public IActionResult CartDetails(int CartId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Cart = cartRepository.GetCart(UserId);
            var Cart = cartRepository.GetCartProducts(CartId);
            return View(Cart);
        }
        public IActionResult DeleteProductFromCart(int CartId,int ProductId)
        {
            cartRepository.DeleteProductFromCart(CartId, ProductId);
            return RedirectToAction("CartDetails", new {CartId});
        }
        
        public IActionResult DeleteCart(int CartId)
        {
            cartRepository.DeleteCart(CartId);
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [Authorize]
        public IActionResult DeleteCartByUserName(string UserName)
        {
            var user = userManager.FindByNameAsync(UserName).Result;
            if (user != null)
            {
                cartRepository.DeleteCartByUserName(UserName);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ConfirmCartDeletion(int NewShopId,int ProductId,int OldCartId)
        {
            cartRepository.DeleteCart(OldCartId);
            string UserName = User.Identity.Name;
            return RedirectToAction("AddProductToCart", new { UserName, ProductId });
        }
        [HttpPost]
        public IActionResult Cancel(int ShopId)
        {
            var shop = cartRepository.GetShopById(ShopId);

            return RedirectToAction("DisplayProductsForUsers", "Search", new { ShopName = shop.Name, id = shop.Id });
        }
 

    }
}
