using User_Order.Model;

namespace User_Order.Repository.IRepository
{
    public interface IUserOrderRepository
    {
        Task AddAsync(UserOrder userOrder);

        Task<IEnumerable<UserOrder>> GetAllAsync();
    }
}
