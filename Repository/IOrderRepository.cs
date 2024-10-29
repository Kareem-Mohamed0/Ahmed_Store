using AhmedStore.Models;

namespace AhmedStore.Repository
{
    public interface IOrderRepository
    {
        Order GetOrderById(int id);
        void AddOrder(Order order);
        void AddOrderProduct(OrderProduct orderProduct);
        void Delete(int OrderId);
        List<Order> GetAll();
        public void MarkStatus(int id, string status);
        public Order GetOrderForInvoice(int id);

    }
}
