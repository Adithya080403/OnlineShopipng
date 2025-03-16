using User_Order.Model;
using User_Order.Model.DTO;

namespace User_Order.Service.IService
{
    public interface IUserOrderService
    {
        Task<bool> AddUserOrderAsync(UserOrderDTO userOrderDTO);

        Task<IEnumerable<UserOrder>> GetAllUserOrderAsync();
    }
}
