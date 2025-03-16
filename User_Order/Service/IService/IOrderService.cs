using User_Order.Model.DTO;
using User_Order.Model;

namespace User_Order.Service.IService
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(OrderDTO orderDTO);

        Task<IEnumerable<Order>> GetAllOrderAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task<Order?> UpdateOrderByIdAsync(int id, OrderDTO orderDTO);

        Task<bool> DeleteOrderByIdAsync(int id);
    }
}
