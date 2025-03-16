using User_Order.Model.DTO;
using User_Order.Model;

namespace User_Order.Service.IService
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDTO userDTO);

        Task<IEnumerable<User>> GetAllUserAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task<User?> UpdateUserByIdAsync(int id, UserDTO userDTO);

        Task<bool> DeleteUserByIdAsync(int id);
    }
}
