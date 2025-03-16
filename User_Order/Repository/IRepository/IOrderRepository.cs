using User_Order.Model;

namespace User_Order.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task UpdateByIdAsync(Order order);

        Task DeleteByIdAsync(Order order);
    }
}
