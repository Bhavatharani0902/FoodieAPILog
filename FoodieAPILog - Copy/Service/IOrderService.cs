using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        void UpdateOrder(int orderId, Order updatedOrder);
        void DeleteOrder(int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        List<Order> GetOrdersByUser(int userId);
    }
}
