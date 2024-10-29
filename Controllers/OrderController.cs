using AhmedStore.Models;
using AhmedStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AhmedStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;

        public OrderController(IOrderRepository orderRepository,ICartRepository cartRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
        }
        public IActionResult PlaceOrder(Order order,Dictionary<int,int> Quantities)
        {
            // 1. Create new Order object
            order.OrderDate = DateTime.Now;

            // 2. Save Order to database (assuming _context is your database context)

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = cartRepository.GetCart(UserId);
            order.UserId = UserId;
            var shopId = cart.ShopId;
            order.ShopId = shopId;
            order.Status = "Pending";
            orderRepository.AddOrder(order);

            // 3. Save each ProductOrder
            
            foreach (var item in cart.CartProducts)
            {
                var OrderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = Quantities[item.ProductId]
                    // Add other relevant fields
                };
                orderRepository.AddOrderProduct(OrderProduct);
            }
            cartRepository.DeleteCart(cart.Id);

            // Redirect or return a view after successful order placement
            return View("OrderConfirmation");
        }
        public IActionResult AllOrdersForAdmin()
        {
            var Orders = orderRepository.GetAll();
            return View(Orders);
        }
        [HttpPost]
        public IActionResult MarkStatus(int id, string status)
        {
            orderRepository.MarkStatus(id, status);

            // Return a JSON response to notify the client of success
            return Json(new { success = true });
        }
        /***============================= Invoice Order =================================***/
        public IActionResult Invoice(int id)
        {
            var Order = orderRepository.GetOrderForInvoice(id);
            return View(Order);
        }
    }
}
