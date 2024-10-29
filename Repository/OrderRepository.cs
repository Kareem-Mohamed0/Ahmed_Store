using AhmedStore.Data;
using AhmedStore.Models;
using Microsoft.EntityFrameworkCore;

namespace AhmedStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void Delete(int OrderId)
        {
            var order = GetOrderById(OrderId);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            var Orders = context.Orders
                .Include(C => C.OrderProducts)
                .ThenInclude(C => C.Product)
                .Include(O => O.Shop)
                .Include(U => U.User)
                .OrderByDescending(U => U.OrderDate)
                .Where(O => O.OrderDate.Day == DateTime.Now.Day)
                .ToList();
            return Orders;

        }

        public Order GetOrderById(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
                return order;
            else throw new Exception(message: "No Orders by This Id");
        }

        void IOrderRepository.AddOrderProduct(OrderProduct orderProduct)
        {
            context.OrderProducts.Add(orderProduct);
            context.SaveChanges();
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
        public Order GetOrderForInvoice(int id)
        {
            var Order = context.Orders
                .Include(C => C.OrderProducts)
                .ThenInclude(C => C.Product)
                .Include(O => O.Shop)
                .Include(U => U.User)
                .FirstOrDefault(O => O.Id == id);
            return Order;
        }
    }
}
