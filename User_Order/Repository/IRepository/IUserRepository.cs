using User_Order.Model;

namespace User_Order.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> AddSync(User user);

        Task<IEnumerable<User>> GetAllSync();

        Task<User?> GetByIdAsync(int id);

        Task UpdateByIdAsync(User user);

        Task DeleteByIdAsync(User user);
    }
}
